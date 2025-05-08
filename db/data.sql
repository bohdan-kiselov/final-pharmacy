USE pharmacy;

INSERT INTO role (role_name) VALUES
('Client'), 
('Admin');  

INSERT INTO users (login, email, password_hash, phone_number)
VALUES (
  'testuser1',
  'testuser1@gmail.com',
  '$2y$10$e0MYzXyjpJS7Pd0RVvHwHeOX5jFJwTh5Df.Yb4uLG2zZG9f6Jf2Hm',
  '+380501234567'
);

INSERT INTO email_verification_tokens (user_id, token, expires_at)
VALUES (
    1,   
    'd03c6e2b-9f1b-4b5f-93c5-5e27a1e3f2da', 
    DATE_ADD(NOW(), INTERVAL 2 MINUTE)
);