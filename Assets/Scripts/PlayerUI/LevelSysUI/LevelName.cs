
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelName : MonoBehaviour
{
    private string TuVi;
    private string giaiDoan;
    public float ExpBonus;
    public float atkBonus;
    public float defBonus;
    public float mpBonus;
    public static LevelName instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }
    public string CanhGioi(int currentLevel)
    {
        var ranges = new List<(int min, int max, List<Action> action)>
        {
            (1, 10, new List<Action>
            {
                () =>
                {
                    TuVi = "Luyện Khí";
                    ExpBonus = 100;
                    atkBonus = 10;
                    defBonus = 10;
                    mpBonus = 10;
                }
            }),
            (11, 20, new List<Action>
            {
                () =>
                {
                    TuVi = "Trúc Cơ"; 
                    ExpBonus = 200;
                    atkBonus = 30;
                    defBonus = 30;
                    mpBonus = 30;
                }
            }),
           (21, 30, new List<Action>
            {
                () =>
                {
                    TuVi = "Kim Đan"; 
                    ExpBonus = 300;
                    atkBonus = 100;
                    defBonus = 100;
                    mpBonus = 100;}
            }),
            (31, 40, new List<Action>
            {
                () => 
                {
                    TuVi = "Nguyên Anh"; 
                    ExpBonus = 400;
                    atkBonus = 1000;
                    defBonus = 1000;
                    mpBonus = 1000;
                }
            }),
            (41, 50, new List<Action>
            {
                () => 
                {
                    TuVi = "Hoá Thần"; 
                    ExpBonus = 500; 
                    atkBonus = 5000;
                    defBonus = 5000;
                    mpBonus = 500;
                }
            }),
            (51, 60, new List<Action>
            {
                () => 
                {
                    TuVi = "Phản Hư"; 
                    ExpBonus = 600; 
                    atkBonus = 10000;
                    defBonus = 10000;
                    mpBonus = 10000;
                }
            }),
            (61, 70, new List<Action>
            {
                () => 
                {
                    TuVi = "Hợp Thể"; 
                    ExpBonus = 700; 
                    atkBonus = 70000;
                    defBonus = 70000;
                    mpBonus  = 70000;
                }
            }),
            (71, 80, new List<Action>
            {
                () => 
                {
                    TuVi = "Độ Kiếp"; 
                    ExpBonus = 800;
                    atkBonus = 100000;
                    defBonus = 100000;
                    mpBonus  = 100000;
                }
            }),
            (81, 90, new List<Action>
            {
                () => 
                {
                    TuVi = "Đại Thừa";
                    ExpBonus = 900; 
                    atkBonus = 1000000;
                    defBonus = 1000000;
                    mpBonus  = 1000000;
                }
            }),
            (91, 100, new List<Action>
            {
                () => 
                {
                    TuVi = "Chân Tiên"; 
                    ExpBonus = 1000; 
                    atkBonus = 999999999;
                    defBonus = 999999999;
                    mpBonus  = 999999999;
                }
            }),
        };
        foreach (var range in ranges)
        {
            if (currentLevel >= range.min && currentLevel <= range.max)
            {
                foreach (var tv in range.action)
                {
                    tv();
                }
                break;
            }
        }
        var chuKy = new List<(int min, int max, Action action)>
        {
            (1, 3, () => giaiDoan = "Sơ Kỳ"),
            (4, 6, () => giaiDoan = "Trung Kỳ"),
            (7, 9, () => giaiDoan = "Hậu Kỳ"),
            (10, 10, () => giaiDoan = "Đỉnh Phong"),
        };
        foreach (var ck in chuKy)
        {
            int a = (currentLevel - 1) % 10 + 1; ;
            if (a >= ck.min && a <= ck.max)
            {
                ck.action();
                break;
            }
        }
        return TuVi + " " + giaiDoan;
    }
}
