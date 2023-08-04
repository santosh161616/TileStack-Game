using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [SerializeField]
    GameObject lostTile;

    float distance;
    float maxDistance;
    float stepLength;
    bool moveForward;
    public bool moveX;

    // Start is called before the first frame update
    void Start()
    {
        maxDistance = 5.0f;
        distance = maxDistance;
        moveForward = false;
        if (moveX)
            transform.Translate(distance, 0, 0);
        else
            transform.Translate(0, 0, distance);
    }

    // Update is called once per frame
    void Update()
    {
        stepLength = Time.deltaTime * 6.0f;
        if (moveX)
            MoveX();
        else
            MoveZ();
    }

    void MoveX()
    {
        if(moveForward)
        {
            if(distance < maxDistance)
            {
                transform.Translate(stepLength, 0, 0);
                distance += stepLength;
            }
            else
            {
                moveForward = false;
            }
        }
        else
        {
            if (distance > -maxDistance)
            {
                transform.Translate(-stepLength, 0, 0);
                distance -= stepLength;
            }
            else
            {
                moveForward = true;
            }
        }
    }

    void MoveZ()
    {
        if (moveForward)
        {
            if (distance < maxDistance)
            {
                transform.Translate(0, 0, stepLength);
                distance += stepLength;
            }
            else
            {
                moveForward = false;
            }
        }
        else
        {
            if (distance > -maxDistance)
            {
                transform.Translate(0, 0, -stepLength);
                distance -= stepLength;
            }
            else
            {
                moveForward = true;
            }
        }
    }

    public void ScaleTile()
    {

        //Cut The Tile
        if(Mathf.Abs(distance) > 0.2f)
        {
            float lostLength = Mathf.Abs(distance);

            if(moveX)
            {
                if(transform.localScale.x < lostLength)
                {
                    gameObject.AddComponent<Rigidbody>();
                    Spawner.instance.GameOver();
                    return;
                }

                GameObject _lostTile = Instantiate(lostTile);
                _lostTile.transform.localScale = new Vector3(lostLength, transform.localScale.y, transform.localScale.z);
                _lostTile.transform.position = new Vector3(transform.position.x
                    + (distance > 0 ? 1 : -1) * (transform.localScale.x - lostLength)/2,
                    transform.position.y,transform.position.z);
                _lostTile.GetComponent<Renderer>().material.SetColor("_Color",
                    GetComponent<Renderer>().material.GetColor("_Color"));

                transform.localScale -= new Vector3(lostLength, 0, 0);
                transform.Translate((distance > 0 ? -1 : 1) * lostLength / 2, 0, 0);
            }
            else
            {
                if (transform.localScale.z < lostLength)
                {
                    gameObject.AddComponent<Rigidbody>();
                    Spawner.instance.GameOver();
                    return;
                }

                GameObject _lostTile = Instantiate(lostTile);
                _lostTile.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, lostLength);
                _lostTile.transform.position = new Vector3(transform.position.x,
                    transform.position.y, transform.position.z
                    + (distance > 0 ? 1 : -1) * (transform.localScale.x - lostLength) / 2);
                _lostTile.GetComponent<Renderer>().material.SetColor("_Color",
                    GetComponent<Renderer>().material.GetColor("_Color"));

                transform.localScale -= new Vector3(0, 0, lostLength);
                transform.Translate(0, 0, (distance > 0 ? -1 : 1) * lostLength / 2);
            }
        }

        //Perfect
        else
        {
            if (moveX)
                transform.Translate(-distance, 0, 0);
            else
                transform.Translate(0, 0, -distance);
        }

        Destroy(this);
    }
}
