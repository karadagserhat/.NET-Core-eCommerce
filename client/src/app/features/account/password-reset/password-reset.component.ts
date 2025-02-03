import { Component, inject } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatCard } from '@angular/material/card';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { AccountService } from '../../../core/services/account.service';
import { SnackbarService } from '../../../core/services/snackbar.service';

@Component({
  selector: 'app-password-reset',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatCard,
    MatFormField,
    MatInput,
    MatLabel,
    MatButton,
  ],
  templateUrl: './password-reset.component.html',
  styleUrl: './password-reset.component.scss',
})
export class PasswordResetComponent {
  accountService = inject(AccountService);
  snackbarService = inject(SnackbarService);

  passwordReset(email: string) {
    this.accountService.passwordReset(email).subscribe({
      next: (params) => {
        this.snackbarService.success('Send Email!');
      },
      error: () => {
        this.snackbarService.error('ERROR!');
      },
    });
  }
}
