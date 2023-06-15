using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialOutline : MonoBehaviour
{
    public Outline joystickL;
    public Outline joystickR;

    public Outline gripL;
    public Outline gripR;

    public Outline triggerL;
    public Outline triggerR;

    public Outline a;

    public Outline book;


    public Outline axe;
    public Outline pickaxe;
    public Outline hammer;

    public Outline tree;
    public Outline stone;
    public Outline gate;

    public Outline buildGround;
    public Outline page2Button;


    public List<Outline> all = new List<Outline>();

    float t = 0;
    float feq = 1;

    private void Start()
    {
        all.Add(joystickL);
        all.Add(joystickR);
        all.Add(gripL);
        all.Add(gripR);
        all.Add(triggerL);
        all.Add(triggerR);
        all.Add(a);
        all.Add(book);
        all.Add(axe);
        all.Add(pickaxe);
        all.Add(hammer);
        all.Add(tree);
        all.Add(stone);
        all.Add(gate);
        all.Add(buildGround);
        all.Add(page2Button);
    }

    private void Update()
    {
        t = Time.time;

        float a = Mathf.Abs((t % feq) - feq / 2);
        float b = a * 10;

        foreach (var i in all)
        {
            i.OutlineWidth = b;
        }
    }

}
