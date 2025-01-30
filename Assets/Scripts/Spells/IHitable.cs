public interface IHitable {
    protected float Health { get; set; }
    public bool Invincible { get; set; }
    public void TakeDamage(float damage);
    public void Die();
}