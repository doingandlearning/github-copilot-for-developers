// Sample legacy code for Demo 2 (Legacy Explanation)
// Synthetic JSF-style example - use for demo only

public class UserBean {
    private String firstName;
    private String lastName;
    private List<String> errors = new ArrayList<>();
    
    public void validateUser() {
        errors.clear();
        if (firstName == null || firstName.trim().isEmpty()) {
            errors.add("First name is required");
        }
        if (lastName == null || lastName.trim().isEmpty()) {
            errors.add("Last name is required");
        }
    }
    
    public boolean isValid() {
        return errors.isEmpty();
    }
    
    public List<String> getErrors() {
        return errors;
    }
    
    // Getters and setters...
    public String getFirstName() { 
        return firstName; 
    }
    
    public void setFirstName(String firstName) { 
        this.firstName = firstName; 
    }
    
    public String getLastName() { 
        return lastName; 
    }
    
    public void setLastName(String lastName) { 
        this.lastName = lastName; 
    }
}
