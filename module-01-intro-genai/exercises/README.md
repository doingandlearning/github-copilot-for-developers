# Module 1 — Exercises

**For delegates.** Complete these during or after the Module 1 session. You can do them on your own or in pairs.

---

## Objective

By completing these exercises, you will:

- Identify where AI can help in your daily work (and where it shouldn't)
- Create a practical "safe use" checklist for your team
- Apply data privacy principles to real scenarios
- Build confidence in using AI tools safely

---

## Scenario

You're a C# developer working on an ASP.NET Core application. Your team is exploring how to use AI tools to improve productivity, but you need to establish clear guidelines about **where**, **when**, and **how** to use AI safely.

These exercises will help you create those guidelines based on what you've learned in Module 1.

---

## Exercise 1: Audit — where would you use AI (and where not)?

**Your task:**

- List **2–3 tasks** you do **today** that could sensibly use AI assistance
- List **2–3 tasks** where you would **not** use AI (or would use it only with extra care)
- Format as a short list or table

**Hints:**

- Think about your actual daily work: code generation, debugging, documentation, testing
- Consider: Is this generic knowledge or proprietary? Does it involve security or compliance?
- Remember: AI is good for patterns and boilerplate; be cautious with security, auth, and internal APIs

**Example format:**

| Could use AI | Would not / be careful |
| ------------- | ----------------------- |
| Writing boilerplate DTOs | Security or auth logic   |
| Explaining a regex       | Decisions that affect compliance |
| Generating unit test stubs | Production configuration |

**Then:** Share with a partner. Compare lists and note one thing you'll try or avoid based on their choices.

**Time:** About 10 minutes.

<details>
<summary>Example Solution for Exercise 1</summary>

**Example answers:**

**Could use AI:**
- Generating boilerplate code (DTOs, entities, repositories)
- Explaining unfamiliar code patterns or libraries
- Writing unit test stubs with Moq
- Refactoring code for readability (with tests to verify)

**Would not / be careful:**
- Security and authentication logic
- Code that handles sensitive data (PII, financial)
- Production configuration and environment variables
- Legal or compliance-related code or documentation
- Internal API integrations (use enterprise AI only)

**Key insight:** If it's generic, well-documented, or testable → AI can help. If it's security-sensitive, proprietary, or compliance-related → verify carefully or avoid.

</details>

---

## Exercise 2: Safe-use checklist for your team

**Your task:**

Draft a **3–5 point "safe use" checklist** for your team. It should cover:

- **Data:** What can be pasted where (public vs. enterprise tools)
- **Verification:** When to double-check or verify AI output (e.g. versions, APIs, security)
- **Escalation:** When to involve someone else or not use AI (e.g. production config, legal text)

**Hints:**

- Keep it practical and actionable
- Focus on the most critical risks (data privacy, security, compliance)
- Make it easy to remember and apply
- Consider your organisation's specific policies

**Format:** Short bullet list.

**Then:** If in a group, share and merge ideas into one team checklist (or note differences if your teams have different policies).

**Time:** About 10–15 minutes.

<details>
<summary>Example Solution for Exercise 2</summary>

**Example checklist:**

1. **Never paste proprietary code or internal APIs into public AI tools** (ChatGPT, public Copilot). Use enterprise instances only.

2. **Always verify API signatures, library versions, and security-sensitive code** before applying AI-generated code to production.

3. **When in doubt about data sensitivity, use enterprise AI or don't send** — better safe than sorry.

4. **For production configuration, security logic, or compliance-related code**, involve a senior developer or security team before using AI assistance.

5. **Test AI-generated code thoroughly**, especially for security, authentication, and data handling.

**Key principles:** Data privacy first, verify critical code, escalate when uncertain.

</details>

---

## Exercise 3: Scenario — public AI OK or enterprise-only?

**Your task:**

For the scenario below, decide whether using **public AI** is acceptable or you would use **enterprise-only** (or not at all). Write **one sentence** explaining why.

**Scenario:**  
*"A colleague wants to ask an AI to explain how an internal REST API works. They would paste the API's OpenAPI spec (which includes endpoint paths and request/response schemas but no real data) into the chat."*

**Hints:**

- Consider: What information is in an OpenAPI spec? (endpoints, schemas, but not actual data)
- Think about: Is this proprietary/internal information? Could it reveal system architecture?
- Remember: Internal APIs are typically considered proprietary, even without real data

**Your answer:**

- [ ] Public AI is OK because …
- [ ] Enterprise-only (or no AI) because …

**Then:** Compare with a partner. If you disagreed, discuss what would change your mind (e.g. if the spec contained customer-facing URLs vs. purely internal paths).

**Time:** About 5–10 minutes.

<details>
<summary>Example Solution for Exercise 3</summary>

**Recommended answer:**

**Enterprise-only (or no AI) because:** Even though the OpenAPI spec doesn't contain real data, it reveals internal API structure, endpoint paths, and data schemas, which are proprietary information about your system architecture.

**Reasoning:**
- Internal APIs are proprietary by definition
- OpenAPI specs reveal system architecture and data models
- This information could be valuable to competitors or attackers
- Public AI tools may retain this information for training

**When public AI might be OK:**
- If the API is already publicly documented (e.g. public-facing API)
- If your organisation explicitly allows it for non-sensitive internal APIs
- If you're using a synthetic/example spec that doesn't match your real system

**Key principle:** When in doubt about whether something is proprietary, use enterprise AI or don't send.

</details>

---

## Final Deliverables

Before moving to Module 2, ensure you have:

- [ ] Completed your audit (Exercise 1) — know where you would and wouldn't use AI
- [ ] Created your safe-use checklist (Exercise 2) — have clear guidelines
- [ ] Analyzed the scenario (Exercise 3) — can apply data privacy principles
- [ ] Shared and discussed with at least one other person

---

## Extensions (nullable reference types)

If you finish early or want to go deeper:

1. **Research your organisation's AI policy:** Find the official policy on using AI tools. How does it align with your checklist?

2. **Create a team wiki page:** Document your safe-use checklist where your team can reference it.

3. **Identify one internal API:** Think of one internal API your team uses. Write a prompt you could use in an enterprise AI tool to get help with it (without pasting the actual code).

4. **Hallucination practice:** Try asking a public AI tool about a very recent library version or feature. See if it hallucinates, and practice verifying the answer.

---

## Key Learning Points

- **Problem-first thinking:** Start by identifying where AI helps vs. where it's risky
- **Practical application:** Checklists and scenarios help you apply principles to real work
- **Verification mindset:** Always verify critical code, especially security and compliance
- **Data privacy first:** When in doubt, use enterprise AI or don't send

---

## After the exercises

- Keep your **audit** and **checklist** somewhere you can reuse (e.g. team wiki or personal notes).
- In Module 2 we'll work on **prompting** — your checklist will help you stay safe while you try new prompts.
- The safety principles from Module 1 apply throughout the course.
