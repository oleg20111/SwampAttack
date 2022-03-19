using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _shootPoint;

    private Weapon _currentWeapon;
    private int _currentHealt;
    private Animator _animator;

    public int Money { get; private set; }

    public event UnityAction<int, int> HealthChanged;

    private void Start()
    {
        _currentWeapon = _weapons[0];
        _currentHealt = _health;
        _animator = GetComponent<Animator>();
    }

    internal void ApplyDamage(int damage)
    {
        _currentHealt -= damage;
        HealthChanged?.Invoke(_currentHealt, _health);

        if (_currentHealt <= 0)
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currentWeapon.Shoot(_shootPoint);
        }
    }

    public void AddMoney(int reward)
    {
        Money += reward;
    }
}