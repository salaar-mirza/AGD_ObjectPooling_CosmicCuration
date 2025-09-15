using System.Collections;
using UnityEngine;
using System;
using CosmicCuration.Utilities;
using System.Collections.Generic;

namespace CosmicCuration.VFX
{
    public class VFXPool : GenericObjectPool<VFXController>
    {
        private VFXView vfxPrefab;

        public VFXPool(VFXView vfxPrefab)
        {
            this.vfxPrefab = vfxPrefab;
        }

        public VFXController GetVFX() => GetItem<VFXController>();

        protected override VFXController CreateItem<T>()
        {
            return new VFXController(vfxPrefab);
        }


        /// Safely try to return an item to this pool. Returns true when the item belonged to this pool.

        public bool TryReturnItem(VFXController controller)
        {
            var pooledItem = pooledItems.Find(i => ReferenceEquals(i.Item, controller));
            if (pooledItem != null)
            {
                pooledItem.isUsed = false;
                return true;
            }
            return false;
        }
    }
}