using UnityEngine;

public class ActionEnum {

    public enum Action
    {
        Nothing = -1,
        Walk = 0,
        GetBottle,
        ThrowBottle,
        Sit,
        Hide,
        ActionDone,
        Leave,
        Kick,
        Slip,
        Stun,
        Drink,
        Dance,
        Dart,
    };

    public struct ActionData
    {
        public Action type;
        public GameObject go;

        public ActionData(Action _type, GameObject _go)
        {
            type = _type;
            go = _go;
        }
    }
}
