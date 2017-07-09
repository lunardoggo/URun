using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ResetTrigger : MonoBehaviour {

    public float yLocationCoordinate = -20.0f;

    private ILevelController controller = null;

    private void Start()
    {
        GameObject gameController = GameObject.FindWithTag("GameController");
        if (gameController.GetComponent<ILevelController>() != null)
        {
            controller = gameController.GetComponent<ILevelController>();
        }
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Update()
    {
        transform.position = new Vector3(controller.Player.transform.position.x, yLocationCoordinate);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        IEntity entity = null;
        if (collider.gameObject.TryGetComponent(out entity))
        {
            entity.Die(DeathCause.Void, null);
        }
        else
        {
            Destroy(collider.gameObject);
        }
    }
}
