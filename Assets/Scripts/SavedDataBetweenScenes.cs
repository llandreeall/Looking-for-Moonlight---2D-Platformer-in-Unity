using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SavedDataBetweenScenes {
    private static int money = 80;
    private static int remainingLives = 3;
    private static int damage = 10;
    private static int speed = 10;


    public static int Money {
        get {
            return money;
        }
        set {
            money = value;
        }
    }

    public static int RemainingLives {
        get {
            return remainingLives;
        }
        set {
            remainingLives = value;
        }
    }

    public static int Damage {
        get {
            return damage;
        }
        set {
            damage = value;
        }
    }

    public static int Speed {
        get {
            return speed;
        }
        set {
            speed = value;
        }
    }
}
