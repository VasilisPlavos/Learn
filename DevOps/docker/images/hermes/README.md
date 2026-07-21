Παράγεις καλά secrets π.χ.:

bash
openssl rand -base64 36
Εκκίνηση
bash
mkdir -p ~/.hermes
docker compose run --rm hermes setup
docker compose up -d
docker compose logs -f hermes
Μετά ανοίγεις:

text
http://IP-ΤΟΥ-HOST:9119
Το /opt/data είναι το persistent state: config, API keys, sessions, skills, memories, logs και όλα τα profiles σου επιβιώνουν σε recreate/update του image.

Profiles
Το default profile υπάρχει στο πρώτο boot. Δημιούργησε τα υπόλοιπα ως εξής:

bash
docker compose exec hermes hermes profile create personal
docker compose exec hermes hermes profile create coder
docker compose exec hermes hermes profile create work
Ξεκίνα τα gateways τους:

bash
docker compose exec hermes hermes -p personal gateway start
docker compose exec hermes hermes -p coder gateway start
docker compose exec hermes hermes -p work gateway start
Έλεγχος:

bash
docker compose exec hermes hermes -p coder gateway status
Κάθε profile έχει δικό του SOUL, credentials, sessions, skills και memory, ενώ το s6 μέσα στο official image αναλαμβάνει restart/crash recovery ανά profile.

Πρόσβαση από έξω
Αν το “έξω από το container” σημαίνει από το ίδιο host ή LAN, το 9119:9119 αρκεί. Αν σημαίνει από το public internet, μην το αφήσεις απλώς ανοιχτό με basic auth:

Καλύτερο για προσωπικό server: Tailscale και publish μόνο στο tailnet.

Εναλλακτικά: Caddy/Traefik/Nginx reverse proxy με HTTPS και OIDC/Cloudflare Access.

Για προσωρινό remote access: SSH tunnel και bind το port μόνο σε localhost:

text
ports:
  - "127.0.0.1:9119:9119"
Η τεκμηρίωση αναφέρει ότι εκτεθειμένα dashboards/API endpoints υπήρξαν attack surface, γι’ αυτό το unauthenticated public dashboard δεν υποστηρίζεται πλέον και αποτυγχάνει κλειστά.

Αν χρειαστείς API
Μόνο αν θέλεις OpenAI API clients, πρόσθεσε στο service:

text
ports:
  - "9119:9119"
  - "8642:8642"

environment:
  API_SERVER_ENABLED: "true"
  API_SERVER_HOST: "0.0.0.0"
  API_SERVER_KEY: "${HERMES_API_KEY}"
Για δεύτερο profile με API, π.χ. work, βάλε στο profile-specific ~/.hermes/profiles/work/.env:

text
API_SERVER_ENABLED=true
API_SERVER_PORT=8643
και δημοσίευσε 8643:8643 στο Compose. Μην ορίσεις global API_SERVER_PORT, γιατί όλα τα profiles θα προσπαθήσουν να κάνουν bind στο ίδιο port.