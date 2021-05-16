using System;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;
    private int health;
    private int healthMax;

    public HealthSystem(int healthMax)
    {
        this.healthMax = healthMax;
        health = healthMax;
    }
    public int GetHealth()
    {
        return health;
    }
    public float GetHealthPercent()//因为用scale来调整血条的情况，血条的range是0到1之间
    {
        return (float)health / (float)healthMax;//需要强制转换成float类型
    }
    public void Damage(int damage)
    {
        health -= damage;
        if (health < 0) { health = 0; }
        if (OnHealthChanged != null) { OnHealthChanged(this, EventArgs.Empty); }
    }
    public void Heal(int heal)
    {
        health += heal;
        if (health > healthMax)
        { health = healthMax; }
        if (OnHealthChanged != null) { OnHealthChanged(this, EventArgs.Empty); }
    }
}
