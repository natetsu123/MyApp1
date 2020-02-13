# MyApp1


# ディレクトリ構造

## App

 * 配布に使えるフォルダ（Installer ）にすれば不要か？

## cmd

 * 何かするためのバッチ

## docs

 * ドキュメント

## Installer

 * インストーラー配置

## src

 * ソース



# ソリューション構造

    - build: デバッグなどの実行ファイルを配置
    - DomainLayer: 
    - InfrastructureLayer
    - packages: NuGet管理
    - WebApp1
    - WinFormsApp1
    - Tests
        - DomainLayer.Tests
        - InfrastructureLyer.Tests
        - WebApp.Tests
        - WinFormsApp.Tests


# ソリューション構造解説

## ApplicatioLayer

アプリケーション層の処理

* データの入出力を行う UI
* 入力データの妥当性チェック
* ドメイン層のコンポーネント呼び出し

## DomainLayer

プレゼンテーション層の処理

* 業務ロジックの実装
* トランザクション管理
* インフラ層のCRUD呼び出し

## InfrastructureLayer

セッション層の処理

* 永続モデルに対してのへのアクセス（CRUD）
* * 永続モデル（DB、Fileなど）
* SQL実装
* O/Rマッパー実装
* 通信機能の実装（IPC, FTP, Socketなど）
