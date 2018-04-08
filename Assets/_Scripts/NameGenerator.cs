using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class NameGenerator
{
    private static ArrayList usedNames = new ArrayList();

    public static string generate()
    {
        string name = "";
        do
        {
            StreamReader reader = new StreamReader("Assets/Resources/text/names.txt");
            int idx = Random.Range(0, 109);
            for (int i = 0; i < idx - 1; i++)
            {
                reader.ReadLine();
            }
            name = reader.ReadLine();
            reader.Close();
        }
        while (usedNames.Contains(name));
        usedNames.Add(name);
        return name;
    }
}
