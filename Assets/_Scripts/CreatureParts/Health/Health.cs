using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Health
{
	public delegate void HealthHandler(Health health, int previousHealth);

	public event HealthHandler HealhDamagedEvent;
	public event HealthHandler HealhHitZeroEvent;
	public event HealthHandler HealhHealedEvent;

	public int MaxPoints { get; private set; }
	public int CurrentPoints { get; private set; }

	public float Percentage { get { return (float)((float)CurrentPoints / (float)MaxPoints); } }
	public bool IsAlive { get { return CurrentPoints > 0; } }

	[SerializeField]
	private int editorSetHealth = 0;


	public Health()
	{
		Initialization(editorSetHealth);
	}

	public Health(int healthpoints)
	{
		Initialization(healthpoints);
    }

	private void Initialization(int healthAmount)
	{
		MaxPoints = CurrentPoints = healthAmount;
	}

	public void SetHealth(int newHealth, bool currentPointsScale = true)
	{
		int oldMax = MaxPoints;
		MaxPoints = newHealth;
		float differents = newHealth / oldMax;
		if(currentPointsScale)
			CurrentPoints = (int)((float)CurrentPoints * differents);
    }

	public void HealFullLife()
	{
		HealHealth(MaxPoints);
	}

	public void Kill()
	{
		DamageHealth(CurrentPoints);
    }

	public void DamageHealth(int dmgAmount)
	{
		dmgAmount = dmgAmount.Clamp(0, dmgAmount);
        int prePoints = CurrentPoints;

		CurrentPoints -= (dmgAmount > CurrentPoints) ? CurrentPoints : dmgAmount;

		if(HealhDamagedEvent != null)
		{
			HealhDamagedEvent(this, prePoints);
        }

		if (!IsAlive)
		{
			if (HealhHitZeroEvent != null)
			{
				HealhHitZeroEvent(this, prePoints);
			}
		}
	}

	public void HealHealth(int healAmount)
	{
		healAmount = healAmount.Clamp(0, healAmount);
		int prePoints = CurrentPoints;
		int newHealth = CurrentPoints + healAmount;

        CurrentPoints = (newHealth > MaxPoints) ? MaxPoints : newHealth;

		if (HealhHealedEvent != null)
		{
			HealhHealedEvent(this, prePoints);
		}
	}
}
