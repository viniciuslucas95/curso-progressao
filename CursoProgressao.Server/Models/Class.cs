﻿namespace CursoProgressao.Server.Models;

public class Class : Model
{
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            UpdateModificationDate();
        }
    }
    public IReadOnlyCollection<Student> Students => _students;
    public IReadOnlyCollection<Contract> Contracts => _contracts;

    private readonly List<Student> _students = new();
    private readonly List<Contract> _contracts = new();
    private string _name;

    public Class(string name) : base() => _name = name;

    public static implicit operator string?(Class? classObj)
        => classObj is not null ? classObj.Name : null;
}
