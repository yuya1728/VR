# 各プログラムで参考にしたサイトと説明

目標:
HMDを装着したままVR空間内のキャラクターをジェスチャやポーズで操作したい

表情・走る・ジャンプ・BGMを流すなど

工夫したところ：

・自分がどの方向を向いていても同じように操作出来るようにした

・出来るだけ小さい動きで操作するようにした

・表情変化するときに手の形も変化するようにした

反省点：

走る・ジャンプの操作があまりうまくいかなかった

作った動画→VR.MP4

## VRTest.cs
コントローラーやトラッカーの位置やボタンによって音を鳴らす・カメラの切り替え・指の
曲がり方について

https://qiita.com/sakano/items/d87a9b11c23a9bbe166f

https://qiita.com/10colo/items/58a8fdb154cd921d4c9c

## Player.cs

ダッシュとジャンプのアニメーションについて

https://unity-shoshinsha.biz/archives/189

## ControllerInputManager.cs

キャラクターの表情の変化について

https://qiita.com/sh_akira/items/81fca69d6f34a42d261c

## ChangeCamera.cs

VRTest.cs から変数を受け取り, カメラの切り替えする

https://gametukurikata.com/camera/changecamera

## InteractableExample.cs

SteamVRのプログラムの一つを変更

掴んだオブジェクトを移動できるようにした

## FingerController.cs

VRM モデルの3D キャラクターの手の形を制御

https://github.com/TORISOUP/AR_VR_Viewer

