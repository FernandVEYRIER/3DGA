using UnityEngine;

public class ActionEnum {

    public enum Action
    {
        Walk,
        GetBottle,
        ThrowBottle,
        Sit,
        Hide,
        ActionDone,
        Leave,
        Kick,
        Slip,
        Stun,
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
