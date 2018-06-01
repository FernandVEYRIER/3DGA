using UnityEngine;

public interface IAnimation
{
    void Initialize(ADrunkAI ai);

    void Walk();

    void GetBottle(GameObject bottle);

    void ThrowBottle();

    void Sit();

    void Hide();

    float Leave();

    void Kick();

    void Slip();

    void Stun();

    void BottleStrick();

    void Dance();

    void Drink();

    void Dart();

    void Fire();
}
