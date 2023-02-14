using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class AttackAera : MonoBehaviour
{
    //[Tooltip("Tags To Store")]
    //public List<string> tags;

    ///// <summary>
    ///// Some objs in list may have been destoryed
    ///// </summary>
    //public List<GameObject> hurtableObjs;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (tags != null)
    //    {
    //        foreach (var tag in tags)
    //        {
    //            IHurtable hurtable = collision.gameObject.GetComponent<IHurtable>();

    //            if (collision.CompareTag(tag) && hurtable != null)
    //            {
    //                hurtableObjs.Add(collision.gameObject);
    //                return;
    //            }
    //        }
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (tags != null)
    //    {
    //        foreach (var tag in tags)
    //        {
    //            IHurtable hurtable = collision.gameObject.GetComponent<IHurtable>();

    //            if (collision.CompareTag(tag) && hurtable != null)
    //            {
    //                hurtableObjs.Remove(collision.gameObject);
    //                return;
    //            }
    //        }
    //    }
    //}

    //public GameObject GetNearest()
    //{
    //    return hurtableObjs.OrderBy(obj => Vector2.Distance(obj.transform.position, transform.position)).FirstOrDefault();
    //}
}
