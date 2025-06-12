using DungeonCrawler.scripts.components.dungeon;

namespace DungeonCrawler.scripts.party;

public class PartyMember {
    
    public Damageable Damageable { get; private set; }

    public void DealDamage(Damageable damageable) {
        damageable.TakeDamage(5); // todo calculate from stats or some shit
    }
}