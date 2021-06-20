using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DequeVector3 
{
    List<Vector3> lv;
    int maxLen = 30;
    int front = 0, back = 0;
    public DequeVector3(int maxlen=30)
    {
        front = back = maxLen / 2;
    }
    public Vector3 getFront()
    {
        return lv[front];
    }
    public Vector3 getBack()
    {
        return lv[back];
    }
    public int pushFront(Vector3 v)
    {
        if (isFull()) return -1;
        lv[front] = v;
        front = (front + maxLen - 1) % maxLen;
        return 0;
    }
    public int pushBack(Vector3 v)
    {
        if (isFull()) return -1;
        back = (back + 1) % maxLen;
        lv[back] = v;
        return 0;
    }
    public int popFront()
    {
        if (isEmpty()) return -1;
        front++;
        return 0;
    }
    public int popBack()
    {
        if (isEmpty()) return -1;
        back--;
        return 0;
    }
    public bool isEmpty()
    {
        return front == back;
    }
    public bool isFull()
    {
        return (back + 1) % maxLen == front;
    }
    
}
