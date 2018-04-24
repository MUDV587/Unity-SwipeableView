﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwipeableView
{
    public class UISwipeableView<TData, TContext> : MonoBehaviour where TContext : class
    {
        [SerializeField]
        private GameObject cardPrefab;

        [SerializeField]
        private Transform cardRoot;


        private List<TData> data = new List<TData>();
        private TContext context;

        private readonly List<UISwipeableCard<TData, TContext>> cards
            = new List<UISwipeableCard<TData, TContext>>(2);



        private UISwipeableCard<TData, TContext> CreateCard()
        {
            var cardObject = Object.Instantiate(cardPrefab, cardRoot);
            cardPrefab.SetActive(true);
            cardObject.transform.SetAsLastSibling();

            var card = cardObject.GetComponent<UISwipeableCard<TData, TContext>>();
            card.SetContext(context);
            card.SetVisible(false);

            return card;
        }

        public void SetContext(TContext context)
        {
            this.context = context;

            for (int i = 0, count = cards.Count; i < count; ++i)
            {
                cards[i].SetContext(context);
            }
        }

        private void UpdateCard(UISwipeableCard<TData, TContext> card, int dataIndex)
        {
            // データが存在しなければ非表示
            if (dataIndex < 0 || dataIndex > data.Count - 1)
            {
                card.SetVisible(false);
                return;
            }

            card.SetVisible(true);
            card.DataIndex = dataIndex;
            card.UpdateContent(data[dataIndex]);
        }
	}
}

