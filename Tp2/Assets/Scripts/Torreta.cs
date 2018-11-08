using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour
{

    public LOS los;

    public Transform target;

    private FSM<Feed> stateMachine;

    public bool shootState;

   // public float contador;

    public List<Rigidbody> balas = new List<Rigidbody>();
    public List<string> bulletType = new List<string>();
    public List<float> weights = new List<float>();
    public Transform barrel;
    public Rigidbody prefab;

    public GameObject smokePs;
    public GameObject flashEfect;

    public ShootingState<Feed> shooting;



    void Start()
    {

        var idle = new IdleState<Feed>();
        shooting = new ShootingState<Feed>(this.transform, target, this);
        var cooldown = new CooldownState<Feed>();

        idle.AddTransition(Feed.EnemigoEntraEnLOS, shooting);

        shooting.AddTransition(Feed.EnemigoSaleDeLOS, idle);
        shooting.AddTransition(Feed.ArmaRecalentada, cooldown);

        cooldown.AddTransition(Feed.ArmaEnfriada, shooting);
        cooldown.AddTransition(Feed.EnemigoSaleDeLOS, idle);

        stateMachine = new FSM<Feed>(idle);

        shootState = true;


    }

    // Update is called once per frame
    void Update()
    {


        stateMachine.Update();

        if (los.IsInSight(target))
        {

            if (shootState == true)
            {
                stateMachine.Feed(Feed.EnemigoEntraEnLOS);
            }



        }

        if (!los.IsInSight(target))
        {
            stateMachine.Feed(Feed.EnemigoSaleDeLOS);
            shooting.contador = 0;
        }

        if (los.IsInSight(target))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.red);
        }

    }

    IEnumerator TransitionToCD()
    {
        stateMachine.Feed(Feed.ArmaRecalentada);
        shootState = false;
        smokePs.SetActive(true);

        yield return new WaitForSeconds(5);

        stateMachine.Feed(Feed.ArmaEnfriada);
        smokePs.SetActive(false);
        shootState = true;
        shooting.contador = 0f;

    }

    public void StartFireCr()
    {
        StartCoroutine(Fire());
    }

    public void StartTransitionToCDCouroutine()
    {
        StartCoroutine(TransitionToCD());
    }

    IEnumerator Fire()
    {
        shooting.allowFire = false;
        var flash = Instantiate(flashEfect, barrel.position, barrel.rotation);
        prefab = balas[GetBullet(bulletType, weights)];
        var bala = Instantiate(prefab, barrel.position, barrel.rotation) as Rigidbody;
        bala.AddForce(new Vector3(this.transform.forward.x, 0, this.transform.forward.z) * 2000, ForceMode.Acceleration);

        yield return new WaitForSeconds(0.5f);

        Destroy(flash);
        shooting.allowFire = true;
    }

    int GetBullet(List<string> color, List<float> weights)
    {


        var rand = Random.value;
        var probabilidadacumulada = 0f;
        var probabilidadDeCadaBala = FixValues(weights);

        for (int i = 0; i < color.Count; i++)
        {
            probabilidadacumulada += probabilidadDeCadaBala[i];
            if (rand < probabilidadacumulada) return color.IndexOf(color[i]);

        }

        return 0;

    }

    List<float> FixValues(List<float> values)
    {
        //calculamos el total de la sumatoria de todos los valores en la lista
        var total = 0f;
        foreach (var item in values)
        {
            total += item;
        }

        //crea la lista para o valores fixeados
        var fixedValues = new List<float>();

        //cada valor fixeado es igual a si mismo dividido el total
        foreach (var item in values)
        {
            fixedValues.Add(item / total);
        }

        return fixedValues;
    }


}

//este enum son las key para el diccionario
public enum Feed
{
    //transiciones de Torreta
    EnemigoEntraEnLOS,
    EnemigoSaleDeLOS,
    ArmaRecalentada,
    ArmaEnfriada,

    //transiciones de Patrol
    EntraEnRangoDeAtaque,
    SaleDeRangoDeAtaque,
    NoHayEnemigos,

    //transiciones de Mine
    BOOOOM
}

