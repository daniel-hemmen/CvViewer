import { Routes } from '@angular/router';
import { Explorer } from './explorer/explorer';
import { Dashboard } from './dashboard/dashboard';

export const routes: Routes = [
    {
        path: '',
        component: Dashboard
    },
    {
        path: 'explorer',
        component: Explorer
    }
];
