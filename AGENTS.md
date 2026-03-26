# Image_Slide_Show - 專案與程式應用分析 (AGENTS.md)

這是一個以 C# 與 Windows Forms (WinForms) 開發的桌面版圖片輪播 (Slide Show) 應用程式。以下是該專案的程式結構與主要應用功能分析：

## 1. 專案基本介紹
- **專案名稱**: Image_SlideShow
- **開發語言與框架**: C# (.NET Framework) / WinForms
- **主要用途**: 在 Windows 桌面上提供一個簡單、無邊框、可自訂時間間隔的圖片輪播顯示工具。

## 2. 核心功能 (Core Features)
- **多種圖片格式支援**: 支援 `.jpg`, `.jpeg`, `.gif`, `.bmp`, `.png`, `.tif` 等常見圖片格式。
- **拖放圖片 (Drag & Drop)**: 使用者可將單一圖片檔案或整個資料夾拖曳至應用程式視窗，程式會背景建立圖片播放清單。
- **隨機/循序播放 (Shuffle/Sequential)**: 支援打亂播放順序 (Shuffle Play)，或是依資料夾讀取順序循序播放，並有防重複的隨機演算法。
- **無邊框與全螢幕**: 可以切換無邊框視窗模式，或是全螢幕輪播，且程式視窗具備拖曳與縮放的機制 (`resizeMode` 與自訂滑鼠事件處理)。
- **播放導覽與歷史紀錄 (Navigation History)**: 
  - 支援「上一張/下一張」的操作體驗，無論是隨機或循序模式，皆能透過歷史紀錄 (`historyIndex`) 追蹤最近播放的圖片（最多保留 32 張歷史軌跡）。
  - **沉浸式回憶探索**: 輪播時可隨時無縫切換 **順序 (Sequential)** 與 **隨機 (Shuffle)** 模式。當隨機抽到某張勾起回憶的照片時，可立刻暫停或切換回順序模式，透過方向鍵前後翻閱當時同一個事件點拍下的周遭照片。
  - 優化隨機權重管理 (`randomPickInx`)，避免同樣的照片過早重複被抽到；所有照片皆播放過後會自動重置權重。
- **豐富的快捷鍵與滑鼠操控**:
  - `S`：暫停/繼續播放（`TogglePause` 透過讀寫 `config.ini [LINK] Pause` 控制）。
  - `空白鍵`：切換隨機 (Random) / 順序 (Sequential) 播放模式。
  - `滑鼠點擊照片(畫面)`：顯示/隱藏周邊 UI (切換無邊框乾淨展示模式)。
  - `左/右方向鍵` 或 `滑鼠滾輪`：手動切換上一張/下一張。
  - `Esc`：展示模式中退出展示；正常模式中關閉程式。
  - `F` / `F11`：切換全螢幕。
- **螢幕顯示 (OSD)**: 在熱鍵操作時提供即時的文字回饋效果（`ShowOsd` + `pictureBox1_Paint` + 2 秒自動消失 `osdTimer`）。
- **組態檔記錄 (config.ini)**: 使用 `.ini` 檔案保存使用者的偏好設定，包含：輪播間隔時間、最上層顯示 (TopMost)、全螢幕、顯示工作列、隨機播放狀態等。
- **匯出播放清單 (Save Image List)**: 可將目前載入的圖片清單匯出為 `.ini` 檔，供日後載入使用（`saveImagesListToolStripMenuItem_Click` → `saveImageList`）。
- **支援贊助 (Donate)**: 內建 PayPalDonate 連結，引導至贊助頁面。

## 3. 隨機/順序模式切換邏輯 (Shuffle/Sequential Switching Logic)

此程式最核心且精巧的設計在於隨機與順序模式之間的無縫切換機制。以下是完整的程式邏輯：

### 3.1 關鍵變數

| 變數 | 型別 | 用途 |
|---|---|---|
| `shufflePlay` | `bool` | 目前是否為隨機模式 |
| `loopInx` | `int` | 目前顯示的圖片在 `fileList` 中的索引 |
| `historyList` | `List<int>` | 隨機模式的播放歷史佇列（存放圖片索引） |
| `historyIndex` | `int` | 目前在 `historyList` 中的游標位置 |
| `fileHitSeq` | `List<UInt64>` | 每張圖片的已播放標記（0 = 未播放） |
| `RandSeq` | `UInt64` | 遞增的播放序號，用來標記播放順序 |
| `MAX_HISTORY_DEPTH` | `const int = 32` | 歷史佇列最大深度 |

### 3.2 順序模式行為 (`shufflePlay == false`)

- **`advanceImage()`**: `loopInx++`，到底後回到 0（循環），直接顯示 `fileList[loopInx]`。
- **`previousImage()`**: `loopInx--`，到頂後回到 `fileList.Count - 1`（循環）。
- **不操作 `historyList`**：順序模式中的前後翻閱不影響隨機歷史紀錄。

### 3.3 隨機模式行為 (`shufflePlay == true`)

- **`advanceImage()`**:
  1. 若 `historyIndex < historyList.Count - 1` → 往前走歷史（重播之前看過的），`historyIndex++`。
  2. 否則 → 呼叫 `randomPickInx()` 產生新的隨機圖片，加入 `historyList` 末端（超過 32 則移除最舊的）。
- **`previousImage()`**: 若 `historyIndex > 0` → `historyIndex--`，回到歷史中的前一張。

### 3.4 `randomPickInx()` 隨機不重複演算法

1. 掃描 `fileHitSeq`，檢查是否所有圖片都已播放（`fileHitSeq[i] != 0`）。
2. 若全部播完 → 全部歸零重置，`RandSeq = 1`，並 OSD 顯示「Random Cycle Reset」。
3. 收集所有 `fileHitSeq[i] == 0` 的未播放索引到 `unplayedIndices`。
4. 從 `unplayedIndices` 中 `rand.Next()` 隨機選一個。
5. 將選中的 `fileHitSeq[inx] = RandSeq++`，標記為已播放。

### 3.5 模式切換行為 (`shuffleClickToONToolStripMenuItem_Click`)

| 切換方向 | 程式行為 | 設計意圖 |
|---|---|---|
| **隨機 → 順序** | 僅設 `shufflePlay = false`，`loopInx` 保持不變 | 使用者可立即用方向鍵探索當前照片「附近」的照片（同一事件/時間） |
| **順序 → 隨機** | 設 `shufflePlay = true`，若 `historyList` 有資料則 `loopInx = historyList[historyIndex]`，恢復到歷史紀錄中的位置 | 探索完畢後切回隨機，會回到原本隨機停留的那張照片，繼續隨機播放 |

### 3.6 `timerLoop_Tick` 自動播放邏輯

1. 每次觸發時，先檢查 `config.ini [LINK] Pause` 是否暫停。
2. 暫停中 → 顯示 `BorderStyle.Fixed3D`，縮短 `timerLoop.Interval = 500` 快速輪詢等待恢復。
3. 播放中 → 呼叫 `advanceImage()`，遵循上述隨機/順序邏輯。
4. 手動按方向鍵或滾輪導覽時，`advanceImage` / `previousImage` 會先 `timerLoop.Stop()` + `timerLoop.Start()` 重置計時器，避免手動操作後立刻自動跳下一張。

## 4. 主要程式碼結構與類別 (Code Structure)

- **`Program.cs`**: 應用程式的進入點，初始化 Visual Styles 並啟動 `Form1` 作為主視窗。
- **`Form1.cs` / `Form1.Designer.cs`**: 主視窗與核心邏輯所在，包含了：
  - 各種 Windows Forms 視窗元件設定 (`PictureBox`, `Timer`, `StatusStrip`, `ToolStripDropDownButton`)。
  - `fetchFileList`: 背景執行緒 (`new Thread`) 負責走訪資料夾中的所有圖檔並建立播放清單，使用 `this.BeginInvoke` 安全地更新 UI。
  - `timerLoop_Tick`, `advanceImage`, `previousImage`: 定時觸發或手動觸發圖片切換邏輯，整合出統一的歷史紀錄 (`historyList`) 導覽設計。
  - `randomPickInx`: 隨機圖片的挑選演算法，從未播放清單中隨機選取，當所有圖片皆顯示後支援權重歸零。
  - `TogglePause`: 暫停/播放切換核心邏輯，透過讀寫 `config.ini [LINK] Pause` 值來控制。
  - `ProcessCmdKey` / `OnMouseWheel`: 處理各種鍵盤快捷鍵以及滑鼠滾輪操作。
  - `ShowOsd`: OSD 提示視窗的渲染邏輯（`pictureBox1_Paint` 繪製半透明背景文字，`osdTimer` 2 秒後自動清除）。
  - `pictureBox1_Click`: 切換無邊框展示模式，同時處理全螢幕/無邊框/背景色的狀態轉換。
  - 滑鼠事件處理 (`pictureBox1_MouseDown`, `pictureBox1_MouseMove`, `pictureBox1_MouseUp`): 實作自定義的拖曳視窗以及無邊框視窗的 8 方位邊緣縮放 (Resize)。
  - `saveImagesListToolStripMenuItem_Click` / `saveImageList`: 匯出目前播放清單為 `.ini` 檔案，使用背景執行緒避免卡頓。
- **`IniRW.cs`**: 封裝了調用 Windows API `WritePrivateProfileString` 與 `GetPrivateProfileString` 的**實例方法**（需 `new IniFile(path)` 後使用），用來讀取與寫入 `.ini` 檔案。
- **`SetDuration.cs` / `SetDuration.Designer.cs`**: 設定輪播間隔時間 (Duration) 的小對話方塊視窗。
- **`AboutSoft.cs` / `AboutSoft.Designer.cs`**: 關於軟體的視窗，顯示軟體版本與相關資訊。
- **`FrmDonate.cs` / `Donate.cs`**: 處理贊助 (Donation) 視窗與 PayPal 按鈕連結功能。

## 5. 特色實作手法 (Technical Highlights)
- **多執行緒防卡頓 (Multi-threading)**: 當大量圖片被拖曳進視窗時，透過開啟新的 `Thread(fetchFileList)` 去處理檔案走訪 (`Directory.GetFiles`)，並使用 `this.BeginInvoke((MethodInvoker)delegate { ... })` 安全地從背景執行緒更新主執行緒 UI，避免跨執行緒存取問題。
- **自定義非標題列拖曳**: 因為將視窗改為無邊框樣式，程式在 `pictureBox1_MouseMove` 計算了游標相對於視窗的像素距離，手動實現了包含 8 個方位的邊緣縮放以及主視窗的區域拖曳效果。
- **Ini 檔案外部載入清單 (`.ini` 清單讀取)**: 支援讀取特殊格式的 `.ini` 檔案（`[IMAGE_SLIDESHOW_LIST]` 區段 + `[PATH]` 區段），當作圖片播放清單的入口。

## 6. 開發約束與行為規範 (Development Constraints)

> 以下規則適用於 AI Agent 修改本專案程式碼時：

### 6.1 跨執行緒 UI 更新
- **必須**使用 `this.BeginInvoke((MethodInvoker)delegate { ... })` 或 `this.Invoke(...)` 從背景執行緒更新 UI。
- **禁止**使用 `Control.CheckForIllegalCrossThreadCalls = false`。

### 6.2 歷史紀錄完整性
- 任何修改 `advanceImage` / `previousImage` / `shuffleClickToONToolStripMenuItem_Click` 的變更，都必須確保 `historyList`、`historyIndex`、`loopInx` 三者的同步一致性。
- 順序模式中的導覽**不可**影響 `historyList` 的內容。
- 模式切換（順序 → 隨機）時**必須**恢復 `loopInx` 到 `historyList[historyIndex]`。

### 6.3 隨機演算法
- `randomPickInx` 必須保證每張圖片在一個 cycle 內只被抽到一次。
- 全部播完後必須重置 `fileHitSeq` 並顯示 OSD「Random Cycle Reset」。

### 6.4 計時器重置
- `advanceImage` 和 `previousImage` 的開頭必須 `timerLoop.Stop()` + `timerLoop.Start()`，確保手動導覽後不會立即自動跳下一張。

### 6.5 檔案清單操作
- 新增圖片到 `fileList` 時，必須同步新增對應的 `fileHitSeq` 項目（初始值 0）。
- `ClearFileList()` 必須同時清空 `fileList`、`fileHitSeq`、`historyList` 並重置 `historyIndex = -1`。

## 7. 建置與發佈流程指南 (Build & Release Workflow)

本專案歷史悠久，原先使用 `.NET Framework 4.0 Client Profile`。在較新的 Visual Studio (例如 VS 2022/2025) 中，預設不再支援該版本的建置，會導致 MSBuild 直接 Skip 建置而失敗。未來若需自動化建置與發佈，請嚴格遵循以下步驟：

### 7.1 環境升級與建置 (Build)
1. **修改目標框架**：修改 `Image_SlideShow.csproj`，將 `<TargetFrameworkVersion>v4.0</TargetFrameworkVersion>` 改為 `v4.8`，並把 `<TargetFrameworkProfile>Client</TargetFrameworkProfile>` 整行註解掉或刪除。
2. **進版號**：更新 `Properties/AssemblyInfo.cs` 中的 `[assembly: AssemblyVersion]` 與 `AssemblyFileVersion`。
3. **使用 devenv.com 建置**：因為舊專案的參考組件關係，單純使用 MSBuild.exe 在新環境下容易解析失敗，請直接調用 Visual Studio 命令列介面 `devenv.com` 執行完整重建。
   > **⚠️ AI Agent 注意事項 (CLI Encoding)**: 為了避免終端機中文亂碼導致 Agent 無法解析編譯錯誤，執行建置時**必須強制加上 `/lcid 1033` 參數**以輸出英文 Log：
   ```powershell
   & "C:\Program Files\Microsoft Visual Studio\18\Community\Common7\IDE\devenv.com" "C:\Github\Image_Slide_Show\Image_SlideShow.sln" /Rebuild Release /lcid 1033
   ```
4. **驗證編譯結果**：檢查 `bin\Release\Image_SlideShow.exe` 是否成功產生，且檔案大小與版本號 (`FileVersion`) 正確。

### 7.2 GitHub 發佈流程 (GitHub Release)
1. **推送到遠端**：將所有 `.csproj` 與 `.cs` 的變更 Commit，並 Push 到 `origin/master`。
2. **建立 Release 與 Tag**：直接透過 GitHub CLI (`gh`) 自動打上 Tag 並上傳新編譯好的執行檔：
   ```powershell
   gh release create vX.X.X.X "C:\Github\Image_Slide_Show\Image_SlideShow\bin\Release\Image_SlideShow.exe" --title "vX.X.X.X" --notes "版本更新說明..." --repo hefenglim/Image_Slide_Show
   ```

## 8. 建議與功能擴充 (Suggestions & Enhancements)
這是一個非常實用的基礎輪播工具，目前已經實現部分進階操控，如果希望追求更強大或更符合現代使用者的需求，可以考慮增加以下功能或進行優化：

### 8.1 實用工具與系統整合
- **刪除目前圖片**：增加一個快速鍵（例如 `Delete` 鍵）或右鍵選單，可以直接將目前正在顯示的圖片移至資源回收桶（適合用來快速整理照片）。
- **多螢幕支援 (Multi-Monitor Support)**：如果使用者有多個螢幕，允許指定輪播要顯示在哪一個螢幕上，而不是預設的主螢幕。
- **開機自動啟動與最小化到系統匣 (System Tray)**：適合將舊電腦或螢幕當作電子相框使用時，開機即可自動隱藏在右下角並開始輪播。
- **資料夾監控 (Folder Monitoring)**：使用 `FileSystemWatcher` 監聽匯入的資料夾，當有新圖片加入時，自動更新播放清單。

### 8.2 程式碼架構與現代化建議
- **升級為 WPF 或 WinUI 3**：如果追求更流暢的硬體加速動畫渲染、現代化 UI 與更進階的視覺效果，可考慮將專案移植至 WPF 或 .NET MAUI / WinUI 3 框架。

## 9. 總結
`Image_Slide_Show` 是一個典型、輕量、具有實用性的 WinForms 小工具範例。它包含了本地端檔案處理、系統 API 呼叫 (設定檔與熱鍵)、多執行緒背景讀取（使用 `BeginInvoke` 安全更新 UI）、自訂視窗樣式操控、自定歷史重播權重等關鍵實務技巧，適合用作學習 C# 與 Windows UI 互動機制的基礎專案。結合上述的建議擴充，它可以變成一個非常完善的桌面電子相框或展場輪播軟體。
