using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


interface ITankMovement {
    void Move();
    void Turn();
    void FixedUpdate();
    //int PlayerNumber { get; set; }
    bool enabled { get; set; }
}

