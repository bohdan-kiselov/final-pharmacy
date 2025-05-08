USE pharmacy;

CREATE TABLE role (
    id INT PRIMARY KEY AUTO_INCREMENT,
    role_name VARCHAR(20) NOT NULL UNIQUE
);

CREATE TABLE users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    login VARCHAR(16) NOT NULL UNIQUE,
    email VARCHAR(254) NOT NULL UNIQUE,
    password_hash CHAR(60) NOT NULL,
    phone_number VARCHAR(13) NOT NULL UNIQUE,
    is_verified BOOLEAN DEFAULT FALSE,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    role_id INT DEFAULT 1,
    FOREIGN KEY (role_id) REFERENCES role(id)
);

CREATE TABLE email_verification_tokens (
    user_id INT NOT NULL,
    token CHAR(36) NOT NULL UNIQUE,
    expires_at DATETIME NOT NULL,
    used BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);