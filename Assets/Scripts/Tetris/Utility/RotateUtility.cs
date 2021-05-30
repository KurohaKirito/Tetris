﻿using System.Collections.Generic;
using System.Linq;
using Tetris.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Tetris.Utility
{
    using TetrisRow = List<Image>;
    
    public static class RotateUtility
    {
        /// <summary>
        /// 旋转判断
        /// </summary>
        /// <param name="shape">形状变量</param>
        /// <param name="backColor">背景色</param>
        /// <typeparam name="T">形状的类型</typeparam>
        /// <returns></returns>
        private static bool RotateJudge<T>(T shape, Sprite backColor) where T : Shape.TetrisShape
        {
            // 先进行预旋转, 返回旋转后的新结点信息
            var newNodes = shape.Rotate(false);
            if (newNodes == null)
            {
                return false;
            }

            // 如果旋转后会造成越界, 则不允许旋转
            if (newNodes.Any(node => node.position.y < NodesManager.ColumnIndex.min) ||
                newNodes.Any(node => node.position.y > NodesManager.ColumnIndex.max) ||
                newNodes.Any(node => node.position.x < NodesManager.RowIndex.min) ||
                newNodes.Any(node => node.position.x > NodesManager.RowIndex.max))
            {
                Debug.Log($"{typeof(T)} <color='red'>越界</color>, 不允许旋转!");
                return false;
            }

            // 如果旋转后会和已存在的结点重叠, 则不允许旋转
            for (var i = 0; i < newNodes.Length; i++)
            {
                var image = NodesManager.GetNodeColor(newNodes[i].position.x, newNodes[i].position.y);
                
                if (image.sprite.Equals(backColor))
                {
                    continue;
                }

                Debug.Log($"{typeof(T)} <color='red'>重叠</color>, 不允许旋转!");
                return false;
            }

            // 通过所有限制条件, 允许旋转
            return true;
        }

        /// <summary>
        /// 让特定形状旋转
        /// </summary>
        /// <param name="shape">形状变量</param>
        /// <param name="backColor">背景颜色</param>
        /// <typeparam name="T">形状类型</typeparam>
        public static void Rotate<T>(T shape, Sprite backColor) where T : Shape.TetrisShape
        {
            // 擦除旧形状
            foreach (var node in shape.GetNodesInfo())
            {
                NodesManager.GetNodeColor(node.position.x, node.position.y).sprite = backColor;
            }

            // 旋转
            if (RotateJudge(shape, backColor))
            {
                shape.Rotate(true);
            }

            // 绘制新形状
            foreach (var node in shape.GetNodesInfo())
            {
                NodesManager.GetNodeColor(node.position.x, node.position.y).sprite = node.color;
            }
        }
    }
}