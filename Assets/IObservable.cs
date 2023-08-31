using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObservable{
    void addObserver(IObserver observer);
    void removeObserver(IObserver observer);
    void notifyObservers(IObservable context);
}
