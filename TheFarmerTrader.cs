using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheFarmer;

public class TheFarmerTrader : Trader
{
    public void StartTheFarmer()
    {
        this.m_animator = this.GetComponentInChildren<Animator>();
        this.m_lookAt = this.GetComponentInChildren<LookAt>();
        this.InvokeRepeating("RandomTalk", this.m_randomTalkInterval, this.m_randomTalkInterval);
    }

    public void UpdateTheFarmer()
    {
        Player closestPlayer = Player.GetClosestPlayer(this.transform.position, this.m_standRange);
        if ((bool)(UnityEngine.Object)closestPlayer)
        {
            this.m_animator.SetBool("Stand", true);
            this.m_lookAt.SetLoockAtTarget(closestPlayer.GetHeadPoint());
            float num = Vector3.Distance(closestPlayer.transform.position, this.transform.position);
            if (!this.m_didGreet && (double)num < (double)this.m_greetRange)
            {
                this.m_didGreet = true;
                this.SayTheFarmer(this.m_randomGreets, "Greet");
                this.m_randomGreetFX.Create(this.transform.position, Quaternion.identity);
            }

            if (!this.m_didGreet || this.m_didGoodbye || (double)num <= (double)this.m_byeRange)
                return;
            this.m_didGoodbye = true;
            this.SayTheFarmer(this.m_randomGoodbye, "Greet");
            this.m_randomGoodbyeFX.Create(this.transform.position, Quaternion.identity);
        }
        else
        {
            this.m_animator.SetBool("Stand", false);
            this.m_lookAt.ResetTarget();
        }
    }

    public void RandomTalkTheFarmer()
    {
        if (!this.m_animator.GetBool("Stand") || StoreGui.IsVisible() ||
            !Player.IsPlayerInRange(this.transform.position, this.m_greetRange))
            return;
        this.SayTheFarmer(this.m_randomTalk, "Talk");
        this.m_randomTalkFX.Create(this.transform.position, Quaternion.identity);
    }

    public string GetHoverTextTheFarmer() =>
        Localization.instance.Localize(this.m_name + "\n[<color=yellow><b>$KEY_Use</b></color>] $raven_interact");

    public string GetHoverNameTheFarmer() => Localization.instance.Localize(this.m_name);

    public bool InteractTheFarmer(Humanoid character, bool hold, bool alt)
    {
        if (hold)
            return false;
        StoreGui.instance.Show(this);
        this.SayTheFarmer(this.m_randomStartTrade, "Talk");
        this.m_randomStartTradeFX.Create(this.transform.position, Quaternion.identity);
        return false;
    }

    public void DiscoverItemsTheFarmer(Player player)
    {
        foreach (Trader.TradeItem availableItem in this.GetAvailableItemsTheFarmer())
            player.AddKnownItem(availableItem.m_prefab.m_itemData);
    }

    public void SayTheFarmer(List<string> texts, string trigger) =>
        this.SayTheFarmer(texts[UnityEngine.Random.Range(0, texts.Count)], trigger);

    public void SayTheFarmer(string text, string trigger)
    {
        Chat.instance.SetNpcText(this.gameObject, Vector3.up * 1.5f, 20f, this.m_hideDialogDelay, "", text, false);
        if (trigger.Length <= 0)
            return;
        this.m_animator.SetTrigger(trigger);
    }

    public bool UseItemTheFarmer(Humanoid user, ItemDrop.ItemData item) => false;

    public void OnBoughtTheFarmer(Trader.TradeItem item)
    {
        this.SayTheFarmer(this.m_randomBuy, "Buy");
        this.m_randomBuyFX.Create(this.transform.position, Quaternion.identity);
    }

    public void OnSoldTheFarmer()
    {
        this.SayTheFarmer(this.m_randomSell, "Sell");
        this.m_randomSellFX.Create(this.transform.position, Quaternion.identity);
    }

    public List<Trader.TradeItem> GetAvailableItemsTheFarmer()
    {
        List<Trader.TradeItem> availableItems = new List<Trader.TradeItem>();
        foreach (Trader.TradeItem tradeItem in this.m_items)
        {
            if (string.IsNullOrEmpty(tradeItem.m_requiredGlobalKey) ||
                ZoneSystem.instance.GetGlobalKey(tradeItem.m_requiredGlobalKey))
                availableItems.Add(tradeItem);
        }

        return availableItems;
    }
}