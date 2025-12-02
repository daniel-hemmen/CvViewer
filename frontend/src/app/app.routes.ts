import { Routes } from '@angular/router';
import { Explorer } from './explorer/explorer';
import { Dashboard } from './dashboard/dashboard';
import { UploadCv } from './upload-cv/upload-cv';

export const routes: Routes = [
  {
    path: '',
    component: Dashboard,
  },
  {
    path: 'explorer',
    component: Explorer,
  },
  {
    path: 'upload',
    component: UploadCv,
  },
];
