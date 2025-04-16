"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const axios_1 = __importDefault(require("axios"));
const express_1 = __importDefault(require("express"));
const app = (0, express_1.default)();
app.get('/api/v1/:id', (req, res) => __awaiter(void 0, void 0, void 0, function* () {
    const { id } = req.params;
    if (id === '2')
        return res.status(200).json({ result: 2 });
    try {
        const response = yield axios_1.default.get('https://api.chucknorris.io/jokes/random');
        return res.status(200).json(response.data);
    }
    catch (error) {
        return res.status(500).json({ error: 'Error fetching joke from external API' });
    }
}));
exports.default = app;
