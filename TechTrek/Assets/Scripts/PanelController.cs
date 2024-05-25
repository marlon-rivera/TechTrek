using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{

    public GameObject panelFestivo;
    public GameObject panelParo;
    public GameObject panelTutoria;
    public GameObject panelPresentacion;
    public GameObject panelMalex;
    public GameObject panelDatus;
    public GameObject panelCyber;
    public GameObject panelErrorus;

    public void ActiveFestivo()
    {
        panelFestivo.SetActive(true);
    }

    public void InactiveFestivo()
    {
        panelFestivo.SetActive(false);
    }

    public void ActiveParo()
    {
        panelParo.SetActive(true);
    }

    public void InactiveParo()
    {
        panelParo.SetActive(false);
    }

    public void ActiveTutoria()
    {
        panelTutoria.SetActive(true);
    }

    public void InactiveTutoria()
    {
        panelTutoria.SetActive(false);
    }

    public void ActivePresentacion(){
        panelPresentacion.SetActive(true);
    }

    public void InactivePresentacion(){
        panelPresentacion.SetActive(false);
    }

    public void ActiveMalex(){
        panelMalex.SetActive(true);
    }

    public void InactiveMalex(){
        panelMalex.SetActive(false);
    }

    public void ActiveDatus(){
        panelDatus.SetActive(true);
    }

    public void InactiveDatus(){
        panelDatus.SetActive(false);
    }

    public void ActiveCyber(){
        panelCyber.SetActive(true);
    }

    public void InactiveCyber(){
        panelCyber.SetActive(false);
    }

    public void ActiveErrorus(){
        panelErrorus.SetActive(true);
    }

    public void InactiveErrorus(){
        panelErrorus.SetActive(false);
    }
}
