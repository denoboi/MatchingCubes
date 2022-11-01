using System;

public static class Events
{
    //Put your events here.

    public static readonly Event<IStackable> OnLastStackableChanged = new Event<IStackable>();
    public static readonly Event<bool> OnSpeedBoostChanged = new Event<bool>();
}