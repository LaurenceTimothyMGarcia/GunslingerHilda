using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BulletPowerProps : ScriptableObject
{
    public MeshRenderer bulletModel;

    public bool fireBullets;

    public bool homingBullets;

    public bool piercingShots;

    public bool stunBullets;

    public bool slowBullets;

    public bool knockbackBullets;
}
