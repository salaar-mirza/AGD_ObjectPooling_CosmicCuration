using System;
using CosmicCuration.Utilities;
using CosmicCuration.PowerUps;

public class PowerUpPool : GenericObjectPool<IPowerUp>
{
    private PowerUpData dataToUse;

    public IPowerUp GetPowerUp<T>(PowerUpData powerUpData) where T : IPowerUp
    {
        dataToUse = powerUpData;
        return GetItem<T>();
    }

    protected override IPowerUp CreateItem<T>()
    {
        if (typeof(T) == typeof(Shield))
            return new Shield(dataToUse);
        else if (typeof(T) == typeof(DoubleTurret))
            return new DoubleTurret(dataToUse);
        else if (typeof(T) == typeof(RapidFire))
            return new RapidFire(dataToUse);
        else
            throw new NotSupportedException($"Power-up type '{typeof(T)}' is not supported.");
    }

}