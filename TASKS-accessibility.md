# Task Tracking: Accessibility Remediation for www.kevincunningham.co.uk

**Audit Date:** May 7, 2026  
**Start Date:** [TBD]  
**Target Completion:** 1–2 weeks (major issues: 1 week; minor issues: 2 weeks)

---

## Phase 1: Critical Fixes (MUST DO) ⬜

### Priority 1: Hamburger Menu Label (1 hour)

- [ ] Add `aria-label="Open navigation menu"` to hamburger button
- [ ] Add `aria-expanded` attribute; toggle on menu open/close
- [ ] Test with screen reader (NVDA/JAWS/VoiceOver)
- [ ] Verify keyboard navigation (Tab + Space to activate)
- **WCAG:** 1.3.1 (A), 2.4.3 (AA)
- **Status:** Not Started

### Priority 2: Blog Card Images (1.5 hours)

- [ ] Create thumbnail image for first blog card (fstrings-tutorial)
- [ ] Update alt text for robot image: "Illustration of a robot representing AI replacing teachers"
- [ ] Update alt text for three-questions image: "Three key questions for course attendees"
- [ ] Test: Disable images in browser; verify alt text is readable
- [ ] Test with screen reader
- **WCAG:** 1.1.1 (A)
- **Status:** Not Started

### Priority 3: Scroll Button Disabled Styling (30 min)

- [ ] Check CSS; ensure `.scroll-btn:disabled { opacity: 0.5; cursor: not-allowed; }`
- [ ] Verify visual distinction is clear in both light & dark modes
- [ ] (Optional) Add `aria-disabled` toggle in JavaScript
- [ ] Test: Tab to buttons; verify focus visible & disabled state clear
- **WCAG:** 2.1.1 (A), 2.1.3 (AAA)
- **Status:** Not Started

---

## Phase 2: Enhancement Fixes (SHOULD DO) ⬜

### Priority 4: Social Media Icon SVGs (30 min)

- [ ] Add `<title>` inside each SVG (Bluesky, Mastodon, GitHub, LinkedIn)
- [ ] Add `<desc>` inside each SVG with link purpose
- [ ] Verify `aria-label` on parent links still works
- **WCAG:** 1.1.1 (A)
- **Status:** Not Started

### Priority 5: Emoji Accessibility Labels (30 min)

- [ ] Add `aria-label` to 👋 (Welcome): "waving hand"
- [ ] Add `aria-label` to ✍️ (I write): "writing"
- [ ] Add `aria-label` to 🤓 (I teach): "thinking face"
- [ ] Add `aria-label` to 📺 (YouTube): "television"
- [ ] Test with screen reader
- **WCAG:** 1.3.1 (A)
- **Status:** Not Started

### Priority 6: Required Form Field Indicators (30 min)

- [ ] Add visual asterisk `*` next to "Name:" and "Email:" labels
- [ ] Apply `aria-label="required"` to asterisk
- [ ] Test with screen reader
- **WCAG:** 3.3.2 (A)
- **Status:** Not Started

---

## Phase 3: Verification & Testing ⬜

### Testing Suite (2 hours)

- [ ] Run axe DevTools scan; verify 0 critical issues
- [ ] Run WAVE audit; verify 0 errors, < 5 warnings
- [ ] Keyboard navigation: Tab through entire page
- [ ] Screen reader test (NVDA/JAWS/VoiceOver): walk through all sections
- [ ] Color contrast check: verify 4.5:1 minimum
- [ ] Visual regression: compare before/after screenshots

### Documentation

- [ ] Update README with accessibility commitment
- [ ] Add accessibility testing checklist to code review process
- [ ] Document tools used (axe, WAVE, NVDA)

---

## Effort Summary

| Phase                    | Tasks         | Est. Time  | Priority      |
| ------------------------ | ------------- | ---------- | ------------- |
| **Phase 1: Critical**    | 3 fixes       | 3 hours    | MUST DO       |
| **Phase 2: Enhancement** | 3 fixes       | 1.5 hours  | SHOULD DO     |
| **Phase 3: Testing**     | Verify + docs | 2 hours    | ESSENTIAL     |
| **TOTAL**                | 6 fixes       | ~6.5 hours | **1–2 weeks** |

---

## Notes

- **Phase 1** should be completed ASAP (this week) to achieve AA compliance.
- **Phase 2** can be done in parallel or following Phase 1.
- **Phase 3** testing should happen after each phase.
- All changes should be reviewed for visual/functional regressions.

---

## Success Criteria

✅ **Done when:**

- [ ] All Phase 1 issues fixed
- [ ] Accessibility testing passes (axe, WAVE, manual screen reader)
- [ ] WCAG AA compliance achieved across homepage
- [ ] No new accessibility issues introduced
- [ ] Team trained on accessibility best practices

---

## Tools & Resources

- **Automated:** axe DevTools, WAVE, Lighthouse
- **Manual:** NVDA (Windows), VoiceOver (Mac), JAWS (commercial)
- **Reference:** WCAG 2.1 Spec, WebAIM articles
