const express = require('express');
const app = express();
const PORT = process.env.PORT || 4000;

app.use(express.json());

app.post('/webhook', (req, res) => {
    console.log('Received Webhook:', req.body);
    res.status(200).send('Webhook received');
});

app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});
