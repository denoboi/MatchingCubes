using System;

public static class Events
{
    //Put your events here.

    public static readonly Event<IStackable> OnLastStackableChanged = new Event<IStackable>();
}