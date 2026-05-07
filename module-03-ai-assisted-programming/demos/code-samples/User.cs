// Sample User class for Demo 3 (Refactoring)
// Synthetic example - use for demo only

public class User {
    private String name;
    private String email;
    private boolean active;
    
    public User(String name, String email, boolean active) {
        this.name = name;
        this.email = email;
        this.active = active;
    }
    
    public String getName() { return name; }
    public String getEmail() { return email; }
    public boolean isActive() { return active; }
}
