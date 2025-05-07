using BepInEx;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace FemboySurvivalCheats
{
    [BepInPlugin( PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION )]
    [BepInProcess( "Femboy Survival.exe" )]
    public class Plugin : BaseUnityPlugin
    {
        public static bool playerInvulnerable = false;
        public static bool playerIntangible = false;
        public static bool alwaysCanMasturbate = false;
        public static bool menuOpen = true;

        public static bool forceGalleryUnlock = true;

        public static bool noclip = false;

#pragma warning disable IDE0051 // Private methods not accessed (it's a Unity thing)
        /// <summary>
        /// Called once loaded
        /// </summary>
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo( $"Plugin {PluginInfo.PLUGIN_GUID} is loaded!" );

            var harmony = new Harmony( PluginInfo.PLUGIN_GUID );
            harmony.PatchAll();
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        private void Update()
        {
            if ( Input.GetKeyDown( KeyCode.O ) )
            {
                playerIntangible = !playerIntangible;
            }

            if ( Input.GetKeyDown( KeyCode.P ) )
            {
                playerInvulnerable = !playerInvulnerable;
            }

            if ( Input.GetKeyDown( KeyCode.F1 ) )
            {
                menuOpen = !menuOpen;
            }

            if ( Input.GetKeyDown( KeyCode.V ) )
            {
                alwaysCanMasturbate = !alwaysCanMasturbate;
            }

            if ( Input.GetKeyDown( KeyCode.LeftBracket ) )
            {
                forceGalleryUnlock = !forceGalleryUnlock;
            }

            if ( Input.GetKeyDown( KeyCode.K ) )
            {
                this.KillAllEnemies();
            }

            if ( Input.GetKeyDown( KeyCode.L ) )
            {
                this.EndEvents();
            }

            if ( Input.GetKeyDown( KeyCode.J ) )
            {
                this.MaxPlayerHealth();
            }

            if ( Input.GetKeyDown( KeyCode.I ) )
            {
                this.MaxWaveCountdown();
            }

            if ( Input.GetKeyDown( KeyCode.Y ) )
            {
                this.LevelUpNormal();
            }

            if ( Input.GetKeyDown( KeyCode.U ) )
            {
                this.LevelUpSex();
            }

            if ( Input.GetKeyDown( KeyCode.Semicolon ) )
            {
                this.AddGold( 1000 );
            }

            int obstacleLayer = 1024;
            int converted = ( int )System.Math.Log( obstacleLayer, 2 );

            if ( Player.instance?.gameObject != null )
            {
                if ( noclip )
                {
                    Physics2D.IgnoreLayerCollision( Player.instance.gameObject.layer, converted, true );
                }
                else
                {
                    Physics2D.IgnoreLayerCollision( Player.instance.gameObject.layer, converted, false );
                }
            }
        }

        /// <summary>
        /// Called for rendering and handling GUI events
        /// </summary>
        private void OnGUI()
#pragma warning restore IDE0051
        {
            if ( menuOpen )
            {
                GUI.Box( new Rect( 10, 10, 170, 345 ), "Cheats" );

                GUI.Label( new( 20, 25, 170, 20 ), "Toggle menu with F1" );

                playerIntangible = GUI.Toggle( new( 20, 40, 150, 20 ), playerIntangible, "Untouchable" );

                playerInvulnerable = GUI.Toggle( new( 20, 60, 150, 20 ), playerInvulnerable, "No hurt" );

                alwaysCanMasturbate = GUI.Toggle( new( 20, 80, 150, 20 ), alwaysCanMasturbate, "Always can masturbate" );

                forceGalleryUnlock = GUI.Toggle( new( 20, 100, 150, 20 ), forceGalleryUnlock, "Unlock gallery" );

                noclip = GUI.Toggle( new( 20, 120, 150, 20 ), noclip, "Noclip" );

                if ( GUI.Button( new( 20, 150, 150, 20 ), "Infinite Wave Time" ) )
                {
                    this.MaxWaveCountdown();
                }

                if ( GUI.Button( new( 20, 175, 150, 20 ), "Kill all enemies" ) )
                {
                    this.KillAllEnemies();
                }

                if ( GUI.Button( new( 20, 200, 150, 20 ), "Max health" ) )
                {
                    this.MaxPlayerHealth();
                }

                if ( GUI.Button( new( 20, 225, 150, 20 ), "Level Up!" ) )
                {
                    this.LevelUpNormal();
                }

                if ( GUI.Button( new( 20, 250, 150, 20 ), "Slut Level Up!" ) )
                {
                    this.LevelUpSex();
                }

                if ( GUI.Button( new( 20, 275, 150, 20 ), "Remove status effects" ) )
                {
                    this.RemoveAllStatusEffects();
                }

                if ( GUI.Button( new( 20, 300, 150, 20 ), "End current event" ) )
                {
                    this.EndEvents();
                }

                string goldAmount = GUI.TextField( new( 20, 325, 65, 20 ), "1000", 6 );
                
                if ( GUI.Button( new( 90, 325, 80, 20 ), "Add gold" ) )
                {
                    this.AddGold( int.Parse( goldAmount ) );
                }
            }
        }

        /// <summary>
        /// Kills any enemy that has spawned in, including bosses
        /// </summary>
        private void KillAllEnemies()
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();

            foreach ( var enemy in enemies )
            {
                Logger.LogInfo( $"Making enemy \"{enemy.name}\" die" );

                enemy.GetComponent<Health>().Kill();
            }

            // Ends any events as well, just in case
            this.EndEvents();
        }

        /// <summary>
        /// Ends any event started by the GrabManager or the CorruptionManager
        /// </summary>
        private void EndEvents()
        {
            GrabManager grabManager = Player.instance.GetComponent<GrabManager>();
            if ( grabManager.IsActive() )
                Traverse.Create( grabManager ).Method( "EndRape" ).GetValue();

            CorruptionManager corruptionManager = Player.instance.GetComponent<CorruptionManager>();
            if ( corruptionManager.IsActive() )
                Traverse.Create( corruptionManager ).Method( "EndEvent" ).GetValue();
        }

        /// <summary>
        /// Sets the player's health to max
        /// </summary>
        private void MaxPlayerHealth()
        {
            PlayerHealth playerHealth = Player.instance.GetComponent<PlayerHealth>();
            playerHealth.SetHealth( playerHealth.GetMaxHealth() );
        }

        /// <summary>
        /// Sets the countdown between waves to max
        /// </summary>
        private void MaxWaveCountdown()
        {
            Logger.LogInfo( "Changing wave countdown to float.PositiveInfinity" );

            EnemySpawner spawnerInstance = EnemySpawner.instance;
            Traverse<float> instanceCountdown = Traverse.Create( spawnerInstance ).Field<float>( "countdown" );

            instanceCountdown.Value = float.PositiveInfinity;
            Logger.LogInfo( $"Set countdown to: {instanceCountdown.Value}!" );
        }

        /// <summary>
        /// Levels up the player's normal stats
        /// </summary>
        private void LevelUpNormal()
        {
            PlayerProgression progression = Player.instance.GetComponent<PlayerProgression>();

            Traverse.Create( progression ).Method( "LevelUp" ).GetValue();
        }

        /// <summary>
        /// Levels up the player's sexual stats
        /// </summary>
        private void LevelUpSex()
        {
            PlayerProgression progression = Player.instance.GetComponent<PlayerProgression>();

            Traverse.Create( progression ).Method( "SexLevelUp" ).GetValue();
        }

        /// <summary>
        /// Removes any status effect currently afflicting the player
        /// </summary>
        private void RemoveAllStatusEffects()
        {
            StatusEffectsManager manager = StatusEffectsManager.instance;

            List<StatusEffect> effects = manager.GetActiveEffects();

            effects.Clear();

            Logger.LogInfo( manager.GetActiveEffects().Count );
        }

        /// <summary>
        /// Adds any amount of gold to the player's inventory
        /// </summary>
        /// <param name="amount">The amount of gold to add</param>
        private void AddGold( int amount )
        {
            Inventory.instance.AddGold( amount );
        }
    }

    /// <summary>
    /// Patches the trigger between any AoE effect, so that it won't affect the player if intangible
    /// </summary>
    [HarmonyPatch( typeof( AreaEffect ), "OnTriggerEnter2D" )]
    class AoETriggerPatch
    {
        static bool Prefix( Collider2D other )
        {
            // Player: StatusEffectsManager
            // Enemy: EnemyStatusEffects
            if ( other.GetComponent<StatusEffectsManager>() == null )
                return true;
            
            return !Plugin.playerIntangible;
        }
    }

    /// <summary>
    /// Patches the collision between Aqua and the player, so that it won't activate if the player is intangible
    /// </summary>
    [HarmonyPatch( typeof( Aqua ), "OnCollisionEnter2D" )]
    class AquaCollisionPatch
    {
        static bool Prefix( Collision2D collision, ref bool ___doHit, int ___hitDamage, Rigidbody2D ___rb, float ___hitPushTweak, float ___hitStunDuration, AquasVoice ___voice, EntityAudioPlayer ___audioPlayer, AudioClip ___hitSound, float ___hitVolume )
        {
            if ( Plugin.playerIntangible ) return false;

            if ( ___doHit )
            {
                Player player = collision.gameObject.GetComponent<Player>();
                if ( player != null )
                {
                    if ( !Plugin.playerInvulnerable )
                    {
                        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

                        playerHealth?.Damage( ___hitDamage, Health.DamageType.Physical );

                        player.ApplyStun( ___hitStunDuration );
                    }
                    
                    Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
                    
                    Vector2 aquaVelocity = ___rb.velocity;
                    aquaVelocity *= ___hitPushTweak;
                    playerRb.AddForce( aquaVelocity, ForceMode2D.Impulse );

                    ___voice.VoiceDashHit();
                    ___audioPlayer.PlayAudioAfterDestruction( ___hitSound, ___hitVolume );
                    ___doHit = false;
                }
            }

            return false;
        }
    }

    /// <summary>
    /// Patches Aqua's grab, so that it won't activate if the player is intangible
    /// </summary>
    [HarmonyPatch( typeof( Aqua_Chasing ), nameof( Aqua_Chasing.Grab ) )]
    class AquaChasingGrabPatch
    {
        static bool Prefix()
        {
            return !Plugin.playerIntangible;
        }
    }

    /// <summary>
    /// Patches the collision between Betty and the player, so that it won't activate if the player is intangible
    /// </summary>
    [HarmonyPatch( typeof( Betty ), "OnCollisionEnter2D" )]
    class BettyCollisionPatch
    {
        static bool Prefix( Collision2D collision, ref bool ___doPunch, int ___punchDamage, Rigidbody2D ___rb, float ___punchPushTweak, float ___punchStunDuration, EntityAudioPlayer ___audioPlayer, AudioClip ___punchHitSound, float ___punchVolume )
        {
            if ( Plugin.playerIntangible ) return false; 

            if ( ___doPunch )
            {
                Player player = collision.gameObject.GetComponent<Player>();
                if ( player != null )
                {
                    if ( !Plugin.playerInvulnerable )
                    {
                        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

                        playerHealth?.Damage( ___punchDamage, Health.DamageType.Physical );
                        
                        player.ApplyStun( ___punchStunDuration );
                    }

                    Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();

                    Vector2 bettyVelocity = ___rb.velocity;
                    bettyVelocity *= ___punchPushTweak;
                    playerRb?.AddForce( bettyVelocity, ForceMode2D.Impulse );

                    ___audioPlayer.PlayAudioAfterDestruction( ___punchHitSound, ___punchVolume );
                    ___doPunch = false;
                }
            }

            return false;
        }
    }

    /// <summary>
    /// Patches Betty's grab, so that it won't activate if the player is intangible
    /// </summary>
    [HarmonyPatch( typeof( Betty_Running ), nameof( Betty_Running.Grab ) )]
    class BettyRunningGrabPatch
    {
        static bool Prefix()
        {
            return !Plugin.playerIntangible;
        }
    }

    /// <summary>
    /// Patches the BunnyGoon Grab collision
    /// </summary>
    [HarmonyPatch( typeof( BunnyGoon_Grabbing ), nameof( BunnyGoon_Grabbing.Grab ) )]
    class GoonGrabPatch
    {
        static bool Prefix()
        {
            return !Plugin.playerIntangible;
        }
    }

    /// <summary>
    /// Patches the BunnyGoon walking grab collision
    /// </summary>
    [HarmonyPatch( typeof( BunnyGoon_Walking ), nameof( BunnyGoon_Walking.Grab ) )]
    class GoonWalkGrabPatch
    {
        static bool Prefix()
        {
            return !Plugin.playerIntangible;
        }
    }

    /// <summary>
    /// Adds a log to the CorruptionManager, so that it displays the name of the corruption event when it starts
    /// </summary>
    [HarmonyPatch( typeof( CorruptionManager ), nameof( CorruptionManager.StartEvent ) )]
    class CorruptionManagerStartEventPatch
    {
        static bool Prefix( CorruptionEvent e )
        {
            Debug.Log( $"Starting corruption event: {e.eventType} ({e.name})" );

            return true;
        }
    }

    /// <summary>
    /// Patches the CorruptionManager, because triggers are used by Aqua's puddles
    /// </summary>
    [HarmonyPatch( typeof( CorruptionManager ), "OnTriggerEnter2D" )]
    class CorruptionManagerTriggerPatch
    {
        static bool Prefix()
        {
            return !Plugin.playerIntangible;
        }
    }

    /// <summary>
    /// Patches the corruption trigger, so that it isn't triggered when the player is intangible
    /// </summary>
    [HarmonyPatch( typeof( CorruptionTrigger ), "OnTriggerEnter2D" )]
    class CorruptionTriggerPatch
    {
        static bool Prefix()
        {
            return !Plugin.playerIntangible;
        }
    }

    /// <summary>
    /// Patches Evelyn's grab, so that it won't activate if the player is intangible
    /// </summary>
    [HarmonyPatch( typeof( Evelyn_WalkToGrab ), nameof( Evelyn_WalkToGrab.Grab ) )]
    class EvelynWalkToGrabPatch
    {
        static bool Prefix( Animator animator )
        {
            if ( !Player.instance.isStunned )
            {
                animator.SetTrigger( "StunEnd" );
                return false;
            }

            return !Plugin.playerIntangible;
        }
    }

    /// <summary>
    /// Patches any explosion (fireball) so that the player won't be hurt if they're invulnerable, and won't be affected if intangible
    /// </summary>
    [HarmonyPatch( typeof( Explosion ), nameof( Explosion.Explode ) )]
    class ExplosionExplodePatch
    {
        static bool Prefix( Explosion __instance, float ___explosionRadius, LayerMask ___enemyLayer, LayerMask ___objectLayer, LayerMask ___playerLayer, int ___damage, float ___explosionForce )
        {
            Collider2D[] enemyColliders = Physics2D.OverlapCircleAll( __instance.transform.position, ___explosionRadius, ___enemyLayer );
            Collider2D[] objectColliders = Physics2D.OverlapCircleAll( __instance.transform.position, ___explosionRadius, ___objectLayer );
            Collider2D playerCollider = Physics2D.OverlapCircle( __instance.transform.position, ___explosionRadius, ___playerLayer );

            // Loop through all enemies
            for ( int i = 0; i < enemyColliders.Length; i++ )
            {
                Health enemyHealth = enemyColliders[ i ].GetComponent<Health>();
                Rigidbody2D enemyRb = enemyColliders[ i ].GetComponent<Rigidbody2D>();
                Animator enemyAnimator = enemyColliders[ i ].GetComponent<Animator>();

                Lily lily = enemyColliders[ i ].GetComponent<Lily>();
                Kaly kaly = enemyColliders[ i ].GetComponent<Kaly>();

                enemyHealth?.Damage( ___damage, Health.DamageType.Magical );
                Debug.Log( $"Fireball dealt {___damage} to {enemyColliders[ i ].name}." );

                Vector2 explosionToEnemy = enemyColliders[ i ].transform.position - __instance.transform.position;
                explosionToEnemy.Normalize();
                explosionToEnemy *= ___explosionForce;
                enemyRb?.AddForce( explosionToEnemy, ForceMode2D.Impulse );
                enemyAnimator?.SetTrigger( "Stun" );

                if ( lily != null || kaly != null )
                {
                    enemyAnimator?.SetBool( "SkipIdle", true );
                }
            }

            // This is where the actual patch happens. The rest is just copied from the Assembly, because I can't exactly decouple this without rewriting
            // If the player is intangible, why bother affecting them?
            if ( playerCollider != null && !Plugin.playerIntangible )
            {
                Rigidbody2D playerRb = playerCollider.GetComponent<Rigidbody2D>();

                // If the player is invulnerable, they shouldn't take damage or be stunned
                if ( !Plugin.playerInvulnerable )
                {
                    Player.instance.GetComponent<Health>().Damage( ___damage, Health.DamageType.Magical );
                    Player.instance.ApplyStun( 1.5f );
                }

                Vector2 explosionToPlayer = Player.instance.transform.position - __instance.transform.position;
                explosionToPlayer.Normalize();
                explosionToPlayer *= ___explosionForce;
                
                playerRb?.AddForce( explosionToPlayer, ForceMode2D.Impulse );
            }

            // Loop through all objects
            for ( int j = 0; j < objectColliders.Length; j++ )
            {
                Health objectHealth = objectColliders[ j ].GetComponent<Health>();
                Rigidbody2D objectRb = objectColliders[ j ].GetComponent<Rigidbody2D>();

                objectHealth?.Damage( ___damage, Health.DamageType.Magical );
                Debug.Log( $"Fireball dealt {___damage} to {objectColliders[ j ].name}." );

                Vector2 explosionToObject = objectColliders[ j ].transform.position - __instance.transform.position;
                explosionToObject.Normalize();
                explosionToObject *= ___explosionForce;
                objectRb?.AddForce( explosionToObject, ForceMode2D.Impulse );
            }

            return false;
        }
    }

    /// <summary>
    /// Patches the Gallery to allow the player forcibly unlock it
    /// </summary>
    [HarmonyPatch( typeof( GalleryTracker ), nameof( GalleryTracker.IsUnlocked ) )]
    class GalleryTrackerUnlockedPatch
    {
        static bool Prefix( ref bool __result )
        {
            if ( Plugin.forceGalleryUnlock )
            {
                __result = true;
                return false;
            }

            return true;
        }
    }

    /// <summary>
    /// Adds a log to GrabManager, so that it displays the name of the enemy raping the player
    /// </summary>
    [HarmonyPatch( typeof( GrabManager ), nameof( GrabManager.StartRape ) )]
#pragma warning disable IDE0300 // For whatever reason, trying to compile without the "new System.Type[]" causes problems, and I don't know why (it worked before)
    [HarmonyPatch( new System.Type[]{ typeof( Enemy ), typeof( GrabAttack ) } )]
#pragma warning restore IDE0300 // So I'll just continue to use this, and tell the IDE to stop complaining
    class GrabManagerStartRapePatch
    {
        static bool Prefix( Enemy e, GrabAttack grab )
        {
            Debug.Log( $"Starting \"{grab?.sceneName}\" rape by: {e.enemyName} ({e.name})" );

            return true;
        }
    }

    /// <summary>
    /// Patches the health trigger, because Bunny uses this to cause damage to the player, and that's not allowed when intangible or invulnerable
    /// </summary>
    [HarmonyPatch( typeof( Health ), "OnTriggerEnter2D" )]
    class HealthTriggerPatch
    {
        static bool Prefix( Health __instance )
        {
            if ( __instance.GetComponent<Player>() == null )
                return true;

            return !( Plugin.playerIntangible || Plugin.playerInvulnerable );
        }
    }

    /// <summary>
    /// Patches the collision between Kaly and the player
    /// </summary>
    [HarmonyPatch( typeof( Kaly ), "OnCollisionEnter2D" )]
    class KalyCollisionPatch
    {
        static bool Prefix( Kaly __instance, Collision2D collision, ref bool ___doChargeHit, Rigidbody2D ___rb, float ___chargePushTweak, float ___chargeStunDuration, int ___chargeDamage, EntityAudioPlayer ___audioPlayer, AudioClip ___chargeHitSound, float ___chargeHitVolume )
        {
            if ( Plugin.playerIntangible ) return false;

            if ( ___doChargeHit )
            {
                Player player = collision.gameObject.GetComponent<Player>();
                if ( player != null )
                {
                    if ( !Plugin.playerInvulnerable )
                    {
                        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

                        playerHealth?.Damage( ___chargeDamage, Health.DamageType.Physical );

                        player.ApplyStun( ___chargeStunDuration );
                    }
                    
                    Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
                    Vector2 kalyVelocity = ___rb.velocity;
                    kalyVelocity *= ___chargePushTweak;
                    playerRb?.AddForce( kalyVelocity, ForceMode2D.Impulse );
                    
                    __instance.StartCoroutine( __instance.DisableHitboxFor( 0.5f ) );
                    ___audioPlayer.PlayAudioAfterDestruction( ___chargeHitSound, ___chargeHitVolume );
                    ___doChargeHit = false;
                }
            }

            return false;
        }
    }

    /// <summary>
    /// Patches the collision between Kaly's grab and the player
    /// </summary>
    [HarmonyPatch( typeof( Kalysea_Walking ), nameof( Kalysea_Walking.Grab ) )]
    class KalyWalkGrabPatch
    {
        static bool Prefix()
        {
            return !Plugin.playerIntangible;
        }
    }

    /// <summary>
    /// Patches Kalysea's shockwave (schockwave) so it doesn't hurt the player when intangible or invulnerable
    /// </summary>
    [HarmonyPatch( typeof( KalyStompSchockwave ), "OnTriggerEnter2D" )]
    class KalyShockwavePatch
    {
        static bool Prefix( Collider2D collision, int ___diceAmmount, int ___strengthBonus, int ___diceSize, float ___stunDuration )
        {
            if ( Plugin.playerIntangible || Plugin.playerInvulnerable ) return false;

            Player player = collision.GetComponent<Player>();
            if ( player != null )
            {
                PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
                int diceRoll = Random.Range( ___diceAmmount + ___strengthBonus, ___diceAmmount * ___diceSize + 1 + ___strengthBonus );
                playerHealth?.Damage( diceRoll, Health.DamageType.Physical );
                player.ApplyStun( ___stunDuration );
            }

            return false;
        }
    }

    /// <summary>
    /// Patches the collision between Betty's lace and the player, so that it won't activate if the player is intangible
    /// </summary>
    [HarmonyPatch( typeof( Lace ), "OnCollisionEnter2D" )]
    class LaceCollisionPatch
    {
        static bool Prefix()
        {
            return !Plugin.playerIntangible;
        }
    }

    /// <summary>
    /// Patches Lily's collision, because intangibility and stuff. Also she kicks so invulnerable checks as well
    /// </summary>
    [HarmonyPatch( typeof( Lily ), "OnCollisionEnter2D" )]
    class LilyCollisionPatch
    {
        static bool Prefix( Collider2D collision, ref bool ___doKick, int ___kickDamage, Rigidbody2D ___rb, float ___kickPushTweak, float ___kickStunDuration, EntityAudioPlayer ___audioPlayer, AudioClip ___kickHitSound, float ___kickHitVolume )
        {
            if ( Plugin.playerIntangible ) return false;

            if ( ___doKick )
            {
                Player player = collision.gameObject.GetComponent<Player>();
                if ( player != null )
                {
                    PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                    Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
                    if ( !Plugin.playerInvulnerable )
                    {
                        playerHealth?.Damage( ___kickDamage, Health.DamageType.Physical );
                    }
                    if ( playerRb != null )
                    {
                        Vector2 lilyVelocity = ___rb.velocity;
                        lilyVelocity *= ___kickPushTweak;
                        playerRb.AddForce( lilyVelocity, ForceMode2D.Impulse );
                    }
                    player.ApplyStun( ___kickStunDuration );
                    ___audioPlayer.PlayAudioAfterDestruction( ___kickHitSound, ___kickHitVolume );
                    ___doKick = false;
                }
            }

            return false;
        }
    }

    /// <summary>
    /// Patches Lily's running grab, intangibility and stuff yk
    /// </summary>
    [HarmonyPatch( typeof( Lily_Running ), nameof( Lily_Running.Grab ) )]
    class LilyGrabPatch
    {
        static bool Prefix()
        {
            return !Plugin.playerIntangible;
        }
    }

    /// <summary>
    /// Patches the collision between Maeve and the player, so that it won't activate if the player is intangible
    /// </summary>
    [HarmonyPatch( typeof( Maeve_Grab ), nameof( Maeve_Grab.Grab ) )]
    class MaeveGrabPatch
    {
        static bool Prefix()
        {
            return !Plugin.playerIntangible;
        }
    }

    /// <summary>
    /// Patches Maeve's running grab collision, which was added sometime between Demo8 and Demo15
    /// </summary>
    [HarmonyPatch( typeof( Maeve_Run ), nameof( Maeve_Run.Grab ) )]
    class MaeveRunGrabPatch
    {
        static bool Prefix()
        {
            return !Plugin.playerIntangible;
        }
    }

    /// <summary>
    /// Patches the collision between any Mimic and the player, so that it won't grab the player if they're intangible
    /// </summary>
    [HarmonyPatch( typeof( Mimic ), nameof( Mimic.Grab ) )]
    class MimicGrabPatch
    {
        static bool Prefix()
        {
            return !Plugin.playerIntangible;
        }
    }

    /// <summary>
    /// Patches the collision between Morgana and the player, so that it won't activate if the player is intangible
    /// </summary>
    [HarmonyPatch( typeof( Morgana_Running ) )]
    [HarmonyPatch( nameof( Morgana_Running.Grab ) )]
    class MorganaRunningGrabPatch
    {
        static bool Prefix()
        {
            return !Plugin.playerIntangible;
        }
    }

    /// <summary>
    /// Patches the PlayerLewd class, so that the player can always masturbate if the variable is set
    /// </summary>
    [HarmonyPatch( typeof( PlayerLewd ), nameof( PlayerLewd.CanMasturbate ) )]
    class PlayerLewdCanMasturbatePatch
    {
        static bool Prefix( ref bool __result )
        {
            if ( Plugin.alwaysCanMasturbate )
            {
                __result = true;
                return false;
            }

            return true;
        }
    }

    /// <summary>
    /// If the player is in a wall, this will attempt to free the player. I don't want it to run if noclip is enabled though
    /// </summary>
    [HarmonyPatch( typeof( UnstuckPlayer ), "CheckIfStuck" )]
    class UnstuckPatch
    {
        static bool Prefix()
        {
            return !Plugin.noclip;
        }
    }

    /// <summary>
    /// Patches the collision between the Vines and the player, so that it won't activate if the player is intangible
    /// </summary>
    [HarmonyPatch( typeof( Vines ), "OnCollisionEnter2D" )]
    class VinesCollisionPatch
    {
        static bool Prefix()
        {
            return !Plugin.playerIntangible;
        }
    }

    /// <summary>
    /// Patches the collision between the Whip and the player, so that it won't activate if the player is intangible
    /// </summary>
    [HarmonyPatch( typeof( Whip ), "OnCollisionEnter2D" )]
    class WhipCollisionPatch
    {
        static bool Prefix( Collision2D collision, ref bool ___hitDone, float ___stunDuration, int ___damage )
        {
            if ( Plugin.playerIntangible ) return false;

            if ( !___hitDone )
            {
                Player player = collision.collider.GetComponent<Player>();
                Health health = collision.collider.GetComponent<Health>();

                // Don't stun an invulnerable player
                if ( !Plugin.playerInvulnerable )
                    player?.ApplyStun( ___stunDuration );

                // Don't damage an invulnerable player, but do damage any enemy
                if ( !Plugin.playerInvulnerable || player == null )
                    health?.Damage( ___damage, Health.DamageType.Physical );

                ___hitDone = true;
            }

            return false;
        }
    }
}
