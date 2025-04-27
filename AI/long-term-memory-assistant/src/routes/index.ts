import { Router } from 'express';
import assistantRoutes from './assistant';

const router = Router();

// Mount assistant routes under /assistant
router.use('/assistant', assistantRoutes);

// Health check route
router.get('/health', (req, res) => {
    res.status(200).json({ status: 'OK', timestamp: new Date().toISOString() });
});

export default router;
