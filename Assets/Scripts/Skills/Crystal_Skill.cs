using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Skill : Skill
{
    [SerializeField] private float crystalDuration;
    [SerializeField] private GameObject crystalPrefab;
    private GameObject currentCrystal;

    [Header("Crystal mirage")]
    [SerializeField] private bool cloneInsteadOfCrystal;

    [Header("Explosive crystal")]
    [SerializeField] private bool canExplode;

    [Header("Moving crystal")]
    [SerializeField] private bool canMoveToEnemy;
    [SerializeField] private float moveSpeed;

    [Header("Multi stacking crystal")]
    [SerializeField] private bool canUseMultiStacks;
    [SerializeField] private int amountOfStacks;
    [SerializeField] private float multiStackCooldown;
    [SerializeField] private float useTimeWindow;
    [SerializeField] private List<GameObject> crystalleft = new List<GameObject>();

    public override void UseSkill()
    {
        base.UseSkill();

        if (CanUseMultiCrystal())
            return;

/*        RefillCrystal();*/

        if(currentCrystal == null)
        {
            CreateCrystal();

        }
        else
        {
            if (canMoveToEnemy)
                return;

            Vector2 playerPos = player.transform.position;
            player.transform.position = currentCrystal.transform.position;

            currentCrystal.transform.position = playerPos;

            if (cloneInsteadOfCrystal)
            {
                SkillManager.Instance.clone.CreateClone(currentCrystal.transform, Vector3.zero);
                Destroy(currentCrystal);
            }
            currentCrystal.GetComponent<Crystal_Skill_Controller>()?.FinishCrystal();

        }
    }

    public void CreateCrystal()
    {
        currentCrystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
        Crystal_Skill_Controller currentCystalScript = currentCrystal.GetComponent<Crystal_Skill_Controller>();

        currentCystalScript.SetupCrystal(crystalDuration, canExplode, canMoveToEnemy, moveSpeed, FindClosestEnemy(currentCrystal.transform), player);

    }

    public void CurrentCrytalChooseRandomTarget() => currentCrystal.GetComponent<Crystal_Skill_Controller>().ChooseRandomEnemy();

    private bool CanUseMultiCrystal()
    {
        if (canUseMultiStacks)
        {
            if (crystalleft.Count>0)
            {
                if (crystalleft.Count == amountOfStacks)
                    Invoke("ResetAbility", useTimeWindow);

                cooldown = 0;
                GameObject crystalToSpawn = crystalleft[crystalleft.Count - 1];
                GameObject newCrystal = Instantiate(crystalToSpawn, player.transform.position, Quaternion.identity);

                crystalleft.Remove(crystalToSpawn);

                newCrystal.GetComponent<Crystal_Skill_Controller>().
                    SetupCrystal(crystalDuration, canExplode, canMoveToEnemy, moveSpeed, FindClosestEnemy(newCrystal.transform), player);

                if (crystalleft.Count<= 0)
                {
                    //cooldown
                    // refull crystal
                    cooldown = multiStackCooldown;
                    RefillCrystal();
                }

            return true;
            }
        }
        return false;
    }
    private void RefillCrystal()
    {
        int amountToAdd = amountOfStacks - crystalleft.Count;
        for(int i =0; i <amountToAdd; i++)
        {
            crystalleft.Add(crystalPrefab);
        }
    }
    private void ResetAbility()
    {
        if (cooldownTimer > 0)
            return;

        cooldownTimer = multiStackCooldown;
        RefillCrystal();
    }
}
