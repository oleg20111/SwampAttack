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
    private int _currentWeaponNumber = 0;
    private int _currentHealt;
    private Animator _animator;

    public int Money { get; private set; }

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChanged;

    private void Start()
    {
        ChangeWeapon(_weapons[_currentWeaponNumber]);
        _currentWeapon = _weapons[0];
        _currentHealt = _health;
        _animator = GetComponent<Animator>();
    }

    public void ApplyDamage(int damage)
    {
        _currentHealt -= damage;
        HealthChanged?.Invoke(_currentHealt, _health);

        if (_currentHealt <= 0)
            Destroy(gameObject);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        _weapons.Add(weapon);
        MoneyChanged?.Invoke(Money);
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
        MoneyChanged?.Invoke(Money);
    }

    public void NextWeapon()
    {
        if (_currentWeaponNumber == _weapons.Count - 1)
            _currentWeaponNumber = 0;
        else
            _currentWeaponNumber++;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    public void PreviousWeapon()
    {
        if (_currentWeaponNumber == 0)
            _currentWeaponNumber = _weapons.Count - 1;
        else
            _currentWeaponNumber--;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }
}