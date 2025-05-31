using DungeonCrawler.scripts.resources;
using Godot;

namespace DungeonCrawler.scripts;

public partial class EnemyNode : Node3D {

    private EnemyResource resource;
    
    public void Init(EnemyResource newResource) {
        resource = newResource;
        // todo health and stats and whatnot
        // todo should rotate to face player when player moves, or when enemy moves
    }
}