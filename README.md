# decode18-xamforms-sample
Xamarin.Forms を使ったデスクトップアプリケーションの展開例です。
Xamarin.Forms はモバイル開発のためだけのものではありません。データアクセス層をはじめとしたコアとなる処理を共有して，デスクトップアプリケーションを異なるプラットフォームに向けて展開することができるということをこのサンプルで示します。

## 前提条件
次の環境を使って構築します。

- Visual Studio 2017 15.7.1 （デスクトップアプリケーション開発）
- Visual Studio for Mac 7.5.1.22
- Xamarin.Mac 4.4.1.178

iOS, Androidについてはこのサンプルでは対象としないため，SDKインストール等は不要です。さらにmacOSのサンプルを実行しないのであれば，macOS環境も不要です。

## サンプルアプリケーション
画像ファイルを入力とし，メディアンカット法による減色を行い，4色カラーパレットを出力するアプリケーションです。次の要素を含みます。

- Xamarin.Forms 3.0 アプリケーションをプロジェクトテンプレートを使わずに構築する
- 共通の処理を .NET Standard 2.0 プロジェクトにまとめる
- 各プラットフォームに処理を委譲する

## プロジェクト構成
- XamForms.DesktopSample
  - XamForms.DesktopSample.Core - UI定義と減色処理
  - XamForms.DesktopSample.Mac - Xamarin.Mac実装
  - XamForms.DesktopSample.Wpf - WPF (Windows Presentation Platform) 実装
  - XamForms.DesktopSample.WinForms - Windows Forms実装

## Core
### 新規.NET Standard 2.0ライブラリプロジェクトを作成する
始めに作成されている `Class1.cs` は削除してかまいません。
### Xamarin.Forms パッケージを導入する
NuGet パッケージを追加します。執筆時点ではバージョン `3.0.0.482510` が最新版でした。
### App.xaml/App.xaml.cs を作成する
`Forms ContentPage` テンプレートを使って作成し， `Application` として振る舞うように書き換えます。
### MainPage.xaml/MainPage.xaml.cs を作成する
`Forms ContentPage` テンプレートを使って作成します。

## macOS
### 新規Cocoa Appプロジェクトを作成する
