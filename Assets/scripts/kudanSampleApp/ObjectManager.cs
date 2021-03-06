﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ObjectManager : MonoBehaviour
{
	public List<GameObject> misObjectos;
	public GameObject BotonBorrar;

	protected GameObject miSeleccionado;

	// Use this for initialization
	void Start ()
	{
        if(BotonBorrar != null)
		    BotonBorrar.SetActive (false);
        
		misObjectos = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
			Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) {
				
				for (int i = 0; i < misObjectos.Count; i++) {

					if (hit.transform.gameObject == misObjectos[i]) {						

						if (miSeleccionado != null && miSeleccionado != hit.transform.gameObject) {
							miSeleccionado.GetComponent<MeshRenderer> ().material.color = Color.white;
						}

						miSeleccionado = misObjectos [i];
						miSeleccionado.GetComponent<MeshRenderer> ().material.color = Color.green;

						BotonBorrar.SetActive (true);

						break;
					}			
				}	
			}
		}
	}

	void OnDrag (DragGesture gesture)
	{		
		if (miSeleccionado != null) {

			if (gesture.Position.x > Screen.width / 3)
				miSeleccionado.transform.localPosition += Vector3.right * gesture.DeltaMove.x + Vector3.forward * gesture.DeltaMove.y;
			else
				miSeleccionado.transform.localRotation *= Quaternion.AngleAxis (gesture.DeltaMove.x * 30.0f * Time.deltaTime, Vector3.up);
		}
	}

	void OnTwist (TwistGesture gesture)
	{
		if (miSeleccionado != null) {
			miSeleccionado.transform.localRotation *= Quaternion.AngleAxis (gesture.DeltaRotation, Vector3.up);
		}
	}

	void OnPinch(PinchGesture gesture)
	{
		if (miSeleccionado != null) {
			miSeleccionado.transform.localScale = Vector3.one * gesture.Delta * 10.0f;
		}
	}


	public void BorrarObjecto()
	{
		if (miSeleccionado != null) {
			misObjectos.Remove (miSeleccionado);
			DestroyObject (miSeleccionado);
			BotonBorrar.SetActive (false);
		}
	}

	public void AdicionarObjeto (GameObject obj)
	{
		misObjectos.Add (obj);
	}
}
