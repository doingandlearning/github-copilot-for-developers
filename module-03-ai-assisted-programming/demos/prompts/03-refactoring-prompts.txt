# Demo 3: Refactoring - Prompts

## Prompt: Refactor to Streams
Refactor this method to use Java 17 Streams API. Replace the nested loops with streams, extract null checks into filters, and improve readability. Maintain the exact same behavior — the method should return the same results.

```java
public List<String> getActiveUserEmails(List<User> users) {
    List<String> emails = new ArrayList<>();
    for (User user : users) {
        if (user != null) {
            if (user.isActive()) {
                String email = user.getEmail();
                if (email != null && !email.isEmpty()) {
                    emails.add(email);
                }
            }
        }
    }
    return emails;
}
```

## If Refactoring Breaks Test: Fix Prompt
Fix the refactoring — the test expects the method to return emails for active users only, filtering out null users and empty emails. The refactored version should maintain this exact behavior.
