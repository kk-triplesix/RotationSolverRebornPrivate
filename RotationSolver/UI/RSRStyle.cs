using Dalamud.Interface.Utility;
using Dalamud.Interface.Utility.Raii;

namespace RotationSolver.UI;

/// <summary>
/// Centralized dark elegant theme for RSR.
/// Replaces all duplicated PushStyle blocks across windows.
/// </summary>
internal static class RSRStyle
{
    // ── Background tiers (deep charcoal) ──
    public static readonly Vector4 BgDeep    = new(0.09f, 0.09f, 0.11f, 1.00f);
    public static readonly Vector4 BgMid     = new(0.13f, 0.13f, 0.15f, 1.00f);
    public static readonly Vector4 BgCard    = new(0.17f, 0.17f, 0.19f, 1.00f);
    public static readonly Vector4 BgRaised  = new(0.21f, 0.21f, 0.24f, 1.00f);

    // ── Accent (muted teal) ──
    public static readonly Vector4 Accent       = new(0.30f, 0.76f, 0.76f, 1.00f);
    public static readonly Vector4 AccentHover  = new(0.38f, 0.86f, 0.86f, 1.00f);
    public static readonly Vector4 AccentActive = new(0.22f, 0.62f, 0.62f, 1.00f);
    public static readonly Vector4 AccentDim    = new(0.30f, 0.76f, 0.76f, 0.45f);
    public static readonly Vector4 AccentSubtle = new(0.30f, 0.76f, 0.76f, 0.15f);

    // ── Text ──
    public static readonly Vector4 TextPrimary   = new(0.90f, 0.91f, 0.93f, 1.00f);
    public static readonly Vector4 TextSecondary  = new(0.58f, 0.59f, 0.63f, 1.00f);
    public static readonly Vector4 TextDisabled   = new(0.38f, 0.39f, 0.43f, 1.00f);

    // ── Sidebar ──
    public static readonly Vector4 SidebarBg     = new(0.07f, 0.07f, 0.09f, 1.00f);
    public static readonly Vector4 SidebarHover  = new(0.15f, 0.15f, 0.18f, 1.00f);
    public static readonly Vector4 SidebarActive = new(0.13f, 0.13f, 0.16f, 1.00f);

    // ── Separator ──
    public static readonly Vector4 SeparatorColor = new(0.22f, 0.22f, 0.26f, 0.60f);

    // ── Tooltip ──
    public static readonly Vector4 TooltipBg     = new(0.11f, 0.11f, 0.14f, 0.96f);
    public static readonly Vector4 TooltipBorder = new(0.30f, 0.76f, 0.76f, 0.35f);

    // ── Section header ──
    public static readonly Vector4 SectionHeaderBg      = new(0.15f, 0.15f, 0.18f, 1.00f);
    public static readonly Vector4 SectionHeaderHover    = new(0.18f, 0.18f, 0.22f, 1.00f);

    // ── Scrollbar ──
    public static readonly Vector4 ScrollBg        = new(0.10f, 0.10f, 0.12f, 0.50f);
    public static readonly Vector4 ScrollGrab      = new(0.26f, 0.26f, 0.30f, 1.00f);
    public static readonly Vector4 ScrollGrabHover = new(0.34f, 0.34f, 0.38f, 1.00f);
    public static readonly Vector4 ScrollGrabActive = new(0.40f, 0.40f, 0.44f, 1.00f);

    // ── Buttons ──
    public static readonly Vector4 Button       = new(0.20f, 0.20f, 0.24f, 1.00f);
    public static readonly Vector4 ButtonHover  = new(0.26f, 0.26f, 0.30f, 1.00f);
    public static readonly Vector4 ButtonActive = new(0.16f, 0.16f, 0.20f, 1.00f);

    // ── Frame (inputs, checkboxes, sliders) ──
    public static readonly Vector4 FrameBg       = new(0.14f, 0.14f, 0.17f, 1.00f);
    public static readonly Vector4 FrameHover    = new(0.19f, 0.19f, 0.22f, 1.00f);
    public static readonly Vector4 FrameActive   = new(0.22f, 0.50f, 0.50f, 0.60f);

    // ── Tabs ──
    public static readonly Vector4 Tab          = new(0.13f, 0.13f, 0.15f, 1.00f);
    public static readonly Vector4 TabHovered   = new(0.26f, 0.60f, 0.60f, 0.80f);
    public static readonly Vector4 TabActive    = new(0.22f, 0.50f, 0.50f, 1.00f);

    // ── Title bar ──
    public static readonly Vector4 TitleBg       = new(0.08f, 0.08f, 0.10f, 1.00f);
    public static readonly Vector4 TitleBgActive = new(0.10f, 0.10f, 0.13f, 1.00f);

    // ── Cached uint colors ──
    private static uint? _accentU32;
    public static uint AccentU32 => _accentU32 ??= ImGui.ColorConvertFloat4ToU32(Accent);

    private static uint? _accentDimU32;
    public static uint AccentDimU32 => _accentDimU32 ??= ImGui.ColorConvertFloat4ToU32(AccentDim);

    private static uint? _accentSubtleU32;
    public static uint AccentSubtleU32 => _accentSubtleU32 ??= ImGui.ColorConvertFloat4ToU32(AccentSubtle);

    private static uint? _sectionHeaderBgU32;
    public static uint SectionHeaderBgU32 => _sectionHeaderBgU32 ??= ImGui.ColorConvertFloat4ToU32(SectionHeaderBg);

    private static uint? _separatorU32;
    public static uint SeparatorU32 => _separatorU32 ??= ImGui.ColorConvertFloat4ToU32(SeparatorColor);

    private static uint? _sidebarBgU32;
    public static uint SidebarBgU32 => _sidebarBgU32 ??= ImGui.ColorConvertFloat4ToU32(SidebarBg);

    /// <summary>
    /// Push the full RSR dark theme. Returns IDisposable that pops everything.
    /// Replaces all duplicated PushStyle blocks across ControlWindow and RotationConfigWindow.
    /// </summary>
    public static ThemeScope PushTheme(float scale = 1f)
    {
        // ── Style vars ──
        ImGui.PushStyleVar(ImGuiStyleVar.SelectableTextAlign, new Vector2(0.5f, 0.5f));
        ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(6, 4) * scale);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(14, 14) * scale);
        ImGui.PushStyleVar(ImGuiStyleVar.CellPadding, new Vector2(6, 3) * scale);
        ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(8, 6) * scale);
        ImGui.PushStyleVar(ImGuiStyleVar.ItemInnerSpacing, new Vector2(6, 4) * scale);
        ImGui.PushStyleVar(ImGuiStyleVar.IndentSpacing, 22f * scale);
        ImGui.PushStyleVar(ImGuiStyleVar.ScrollbarSize, 12f * scale);
        ImGui.PushStyleVar(ImGuiStyleVar.GrabMinSize, 11f * scale);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 1f);
        ImGui.PushStyleVar(ImGuiStyleVar.ChildBorderSize, 0f);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 8f * scale);
        ImGui.PushStyleVar(ImGuiStyleVar.ChildRounding, 6f * scale);
        ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 5f * scale);
        ImGui.PushStyleVar(ImGuiStyleVar.PopupRounding, 6f * scale);
        ImGui.PushStyleVar(ImGuiStyleVar.ScrollbarRounding, 6f * scale);
        ImGui.PushStyleVar(ImGuiStyleVar.GrabRounding, 4f * scale);
        ImGui.PushStyleVar(ImGuiStyleVar.TabRounding, 5f * scale);

        // ── Colors ──
        ImGui.PushStyleColor(ImGuiCol.WindowBg, BgDeep);
        ImGui.PushStyleColor(ImGuiCol.ChildBg, BgMid);
        ImGui.PushStyleColor(ImGuiCol.PopupBg, BgMid);
        ImGui.PushStyleColor(ImGuiCol.Border, SeparatorColor);
        ImGui.PushStyleColor(ImGuiCol.BorderShadow, Vector4.Zero);

        ImGui.PushStyleColor(ImGuiCol.FrameBg, FrameBg);
        ImGui.PushStyleColor(ImGuiCol.FrameBgHovered, FrameHover);
        ImGui.PushStyleColor(ImGuiCol.FrameBgActive, FrameActive);

        ImGui.PushStyleColor(ImGuiCol.TitleBg, TitleBg);
        ImGui.PushStyleColor(ImGuiCol.TitleBgActive, TitleBgActive);
        ImGui.PushStyleColor(ImGuiCol.TitleBgCollapsed, TitleBg);

        ImGui.PushStyleColor(ImGuiCol.Text, TextPrimary);
        ImGui.PushStyleColor(ImGuiCol.TextDisabled, TextDisabled);

        ImGui.PushStyleColor(ImGuiCol.Button, Button);
        ImGui.PushStyleColor(ImGuiCol.ButtonHovered, ButtonHover);
        ImGui.PushStyleColor(ImGuiCol.ButtonActive, ButtonActive);

        ImGui.PushStyleColor(ImGuiCol.Header, BgCard);
        ImGui.PushStyleColor(ImGuiCol.HeaderHovered, BgRaised);
        ImGui.PushStyleColor(ImGuiCol.HeaderActive, AccentActive);

        ImGui.PushStyleColor(ImGuiCol.Separator, SeparatorColor);
        ImGui.PushStyleColor(ImGuiCol.SeparatorHovered, AccentDim);
        ImGui.PushStyleColor(ImGuiCol.SeparatorActive, Accent);

        ImGui.PushStyleColor(ImGuiCol.ScrollbarBg, ScrollBg);
        ImGui.PushStyleColor(ImGuiCol.ScrollbarGrab, ScrollGrab);
        ImGui.PushStyleColor(ImGuiCol.ScrollbarGrabHovered, ScrollGrabHover);
        ImGui.PushStyleColor(ImGuiCol.ScrollbarGrabActive, ScrollGrabActive);

        ImGui.PushStyleColor(ImGuiCol.CheckMark, Accent);
        ImGui.PushStyleColor(ImGuiCol.SliderGrab, Accent);
        ImGui.PushStyleColor(ImGuiCol.SliderGrabActive, AccentActive);

        ImGui.PushStyleColor(ImGuiCol.Tab, Tab);
        ImGui.PushStyleColor(ImGuiCol.TabHovered, TabHovered);
        ImGui.PushStyleColor(ImGuiCol.TabActive, TabActive);
        ImGui.PushStyleColor(ImGuiCol.TabUnfocusedActive, TabActive);

        ImGui.PushStyleColor(ImGuiCol.ResizeGrip, AccentSubtle);
        ImGui.PushStyleColor(ImGuiCol.ResizeGripHovered, AccentDim);
        ImGui.PushStyleColor(ImGuiCol.ResizeGripActive, Accent);

        return new ThemeScope();
    }

    /// <summary>
    /// Draws a vertical accent bar on the left edge (for active sidebar items / section headers).
    /// </summary>
    public static void DrawAccentBar(Vector2 screenPos, float height, float thickness = 3f)
    {
        ImGui.GetWindowDrawList().AddRectFilled(
            screenPos,
            new Vector2(screenPos.X + thickness, screenPos.Y + height),
            AccentU32,
            thickness * 0.5f);
    }

    /// <summary>
    /// Draws a subtle horizontal separator line with theme colors.
    /// </summary>
    public static void ThemedSeparator()
    {
        ImGui.Dummy(new Vector2(0, 2));
        var pos = ImGui.GetCursorScreenPos();
        var width = ImGui.GetContentRegionAvail().X;
        ImGui.GetWindowDrawList().AddLine(
            pos,
            new Vector2(pos.X + width, pos.Y),
            SeparatorU32);
        ImGui.Dummy(new Vector2(0, 4));
    }

    public readonly struct ThemeScope : IDisposable
    {
        private const int StyleVarCount = 18;
        private const int StyleColorCount = 36;

        public void Dispose()
        {
            ImGui.PopStyleColor(StyleColorCount);
            ImGui.PopStyleVar(StyleVarCount);
        }
    }
}
