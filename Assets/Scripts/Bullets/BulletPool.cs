using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmicCuration.Bullets
{
    public class BulletPool
    {
        // Field should be declared here, not inside the constructor
        private BulletView bulletView;
        private BulletScriptableObject bulletScriptableObject;
        private List<PooledBullet> pooledBullets = new List<PooledBullet>();

        public BulletPool(BulletView bulletView, BulletScriptableObject bulletScriptableObject )
        {
            this.bulletView = bulletView;
            this.bulletScriptableObject = bulletScriptableObject;

        }

        public BulletController GetBullet()
        {
            if (pooledBullets.Count > 0)
            {
                PooledBullet pooledBullet = pooledBullets.Find(b => !b.isUded);
                if (pooledBullet != null)
                {
                    pooledBullet.isUded = true;
                    return pooledBullet.bullet;
                }
            }
            return CreateNewPooledBullet();
        }

        private BulletController CreateNewPooledBullet()
        {
            PooledBullet newPooledBullet = new PooledBullet
            {
                bullet = new BulletController(bulletView, bulletScriptableObject),
                isUded = true
                
            };
            pooledBullets.Add(newPooledBullet);
            return newPooledBullet.bullet;
        }

        public void ReturnBullet(BulletController bullet)
        {
            PooledBullet pooledBullet = pooledBullets.Find(b => b.bullet == bullet);
    
                pooledBullet.isUded = false;
            
        }

        public class PooledBullet
        {
            public BulletController bullet;
            public bool isUded;
        }
    }


  
}





