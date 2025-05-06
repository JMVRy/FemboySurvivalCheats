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

#pragma warning disable IDE0051
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo( $"Plugin {PluginInfo.PLUGIN_GUID} is loaded!" );

            var harmony = new Harmony( PluginInfo.PLUGIN_GUID );
            harmony.PatchAll();
        }

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
            /*Boss[] bosses = FindObjectsOfType<Boss>();

            foreach ( var boss in bosses )
            {
                Logger.LogInfo( $"Satisfying boss \"{boss.name}\"");

                boss.SatisfyBoss();
            }*/

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

            System.Collections.Generic.List<StatusEffect> effects = manager.GetActiveEffects();

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

    [HarmonyPatch( typeof( AreaEffect ), "OnTriggerEnter2D" )]
    class AoETriggerPatch
    {
        static bool Prefix( Collider2D other, bool ___affectsPlayer)
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
        static bool Prefix( Collision2D collision, ref bool ___doHit, int ___hitDamage, Rigidbody2D ___rb, float ___hitPushTweak, float ___hitStunDuration, EntityAudioPlayer ___audioPlayer, AudioClip ___hitSound, float ___hitVolume )
        {
            if ( Plugin.playerIntangible ) return false;

            Debug.Log( "(Aqua) Collided with " + collision.gameObject.name );
            if ( ___doHit )
            {
                Player component = collision.gameObject.GetComponent<Player>();
                if ( component != null )
                {
                    PlayerHealth component2 = component.GetComponent<PlayerHealth>();
                    Rigidbody2D component3 = component.GetComponent<Rigidbody2D>();
                    if ( !Plugin.playerInvulnerable )
                    {
                        component2?.Damage( ___hitDamage, Health.DamageType.Physical );
                    }
                    if ( component3 != null )
                    {
                        Vector2 vector = ___rb.velocity;
                        vector *= ___hitPushTweak;
                        component3.AddForce( vector, ForceMode2D.Impulse );
                    }
                    component.ApplyStun( ___hitStunDuration );
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

            Debug.Log( "(Betty) Collided with " + collision.gameObject.name );
            if ( ___doPunch )
            {
                Player component = collision.gameObject.GetComponent<Player>();
                if ( component != null )
                {
                    PlayerHealth component2 = component.GetComponent<PlayerHealth>();
                    Rigidbody2D component3 = component.GetComponent<Rigidbody2D>();
                    if ( !Plugin.playerInvulnerable )
                    {
                        component2?.Damage( ___punchDamage, Health.DamageType.Physical );
                    }
                    if ( component3 != null )
                    {
                        Vector2 vector = ___rb.velocity;
                        vector *= ___punchPushTweak;
                        component3.AddForce( vector, ForceMode2D.Impulse );
                    }
                    component.ApplyStun( ___punchStunDuration );
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

    [HarmonyPatch( typeof( BunnyGoon_Grabbing ), nameof( BunnyGoon_Grabbing.Grab ) )]
    class GoonGrabPatch
    {
        static bool Prefix()
        {
            return !Plugin.playerIntangible;
        }
    }

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
            Collider2D[] array = Physics2D.OverlapCircleAll( __instance.transform.position, ___explosionRadius, ___enemyLayer );
            Collider2D[] array2 = Physics2D.OverlapCircleAll( __instance.transform.position, ___explosionRadius, ___objectLayer );
            Collider2D collider2D = Physics2D.OverlapCircle( __instance.transform.position, ___explosionRadius, ___playerLayer );
            for ( int i = 0; i < array.Length; i++ )
            {
                Health component = array[ i ].GetComponent<Health>();
                Rigidbody2D component2 = array[ i ].GetComponent<Rigidbody2D>();
                Animator component3 = array[ i ].GetComponent<Animator>();
                if ( component != null )
                {
                    component.Damage( ___damage, Health.DamageType.Magical );
                    Debug.Log( $"Fireball dealt {___damage} to {array[ i ].name}." );
                }
                Vector2 vector = array[ i ].transform.position - __instance.transform.position;
                vector.Normalize();
                vector *= ___explosionForce;
                component2?.AddForce( vector, ForceMode2D.Impulse );
                component3?.SetTrigger( "Stun" );
            }
            if ( collider2D != null )
            {
                Rigidbody2D component4 = collider2D.GetComponent<Rigidbody2D>();

                if ( !Plugin.playerInvulnerable )
                {
                    Player.instance.GetComponent<Health>().Damage( ___damage, Health.DamageType.Magical );
                    
                    if ( !Plugin.playerIntangible )
                        Player.instance.ApplyStun( 1.5f );
                }

                if ( !Plugin.playerIntangible )
                {
                    Vector2 vector2 = Player.instance.transform.position - __instance.transform.position;
                    vector2.Normalize();
                    vector2 *= ___explosionForce;
                    
                    component4?.AddForce( vector2, ForceMode2D.Impulse );
                }
            }
            for ( int j = 0; j < array2.Length; j++ )
            {
                Health component5 = array2[ j ].GetComponent<Health>();
                Rigidbody2D component6 = array2[ j ].GetComponent<Rigidbody2D>();
                if ( component5 != null )
                {
                    component5.Damage( ___damage, Health.DamageType.Magical );
                    Debug.Log( $"Fireball dealt {___damage} to {array2[ j ].name}." );
                }
                Vector2 vector3 = array2[ j ].transform.position - __instance.transform.position;
                vector3.Normalize();
                vector3 *= ___explosionForce;
                component6?.AddForce( vector3, ForceMode2D.Impulse );
            }

            return false;
        }
    }

    [HarmonyPatch( typeof( GalleryTracker ), nameof( GalleryTracker.IsUnlocked ) )]
    class GalleryTrackerUnlockedPatch
    {
        static bool Prefix( string sceneName, ref bool __result, List<string> ___seenScenes )
        {
            if ( Plugin.forceGalleryUnlock )
                __result = true;
            else
                __result = ___seenScenes.Contains( sceneName );

            return false;
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
                Player component = collision.gameObject.GetComponent<Player>();
                if ( component != null )
                {
                    PlayerHealth component2 = component.GetComponent<PlayerHealth>();
                    Rigidbody2D component3 = component.GetComponent<Rigidbody2D>();
                    if ( !Plugin.playerInvulnerable )
                    {
                        component2?.Damage( ___chargeDamage, Health.DamageType.Physical );
                    }
                    if ( component3 != null )
                    {
                        Vector2 vector = ___rb.velocity;
                        vector *= ___chargePushTweak;
                        component3.AddForce( vector, ForceMode2D.Impulse );
                    }
                    component.ApplyStun( ___chargeStunDuration );
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

    [HarmonyPatch( typeof( KalyStompSchockwave ), "OnTriggerEnter2D" )]
    class KalyShockwavePatch
    {
        static bool Prefix( Collider2D collision, int ___diceAmmount, int ___strengthBonus, int ___diceSize, float ___stunDuration )
        {
            if ( Plugin.playerIntangible ) return false;

            Player component = collision.GetComponent<Player>();
            if ( component != null && !Plugin.playerIntangible )
            {
                PlayerHealth component2 = collision.GetComponent<PlayerHealth>();
                if ( component2 != null && !Plugin.playerInvulnerable )
                {
                    int num = UnityEngine.Random.Range( ___diceAmmount + ___strengthBonus, ___diceAmmount * ___diceSize + 1 + ___strengthBonus );
                    component2.Damage( num, Health.DamageType.Physical );
                }
                component.ApplyStun( ___stunDuration );
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

    [HarmonyPatch( typeof( Lily ), "OnCollisionEnter2D" )]
    class LilyCollisionPatch
    {
        static bool Prefix( Collider2D collision, ref bool ___doKick, int ___kickDamage, Rigidbody2D ___rb, float ___kickPushTweak, float ___kickStunDuration, EntityAudioPlayer ___audioPlayer, AudioClip ___kickHitSound, float ___kickHitVolume )
        {
            if ( Plugin.playerIntangible ) return false;

            if ( ___doKick )
            {
                Player component = collision.gameObject.GetComponent<Player>();
                if ( component != null )
                {
                    PlayerHealth component2 = component.GetComponent<PlayerHealth>();
                    Rigidbody2D component3 = component.GetComponent<Rigidbody2D>();
                    if ( !Plugin.playerInvulnerable )
                    {
                        component2?.Damage( ___kickDamage, Health.DamageType.Physical );
                    }
                    if ( component3 != null )
                    {
                        Vector2 vector = ___rb.velocity;
                        vector *= ___kickPushTweak;
                        component3.AddForce( vector, ForceMode2D.Impulse );
                    }
                    component.ApplyStun( ___kickStunDuration );
                    ___audioPlayer.PlayAudioAfterDestruction( ___kickHitSound, ___kickHitVolume );
                    ___doKick = false;
                }
            }

            return false;
        }
    }

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
        static bool Prefix( ref bool __result, LewdInfo ___playerLewdInfo, float ___arousal )
        {
            if ( Plugin.alwaysCanMasturbate )
            {
                __result = true;
                return false;
            }

            float num = 50f - 15f * ___playerLewdInfo.masturbationFetish.level;
            __result = ___arousal >= num;

            return false;
        }
    }

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
                Player component = collision.collider.GetComponent<Player>();
                Health component2 = collision.collider.GetComponent<Health>();

                component?.ApplyStun( ___stunDuration );

                if ( !Plugin.playerInvulnerable )
                {
                    component2?.Damage( ___damage, Health.DamageType.Physical );
                }

                ___hitDone = true;
            }

            return false;
        }
    }
}
