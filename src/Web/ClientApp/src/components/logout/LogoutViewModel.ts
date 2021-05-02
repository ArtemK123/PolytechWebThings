export class LogoutViewModel {
  public handleLogout() {
    fetch("api/UserApi/Logout", {
      method: "POST"
    }).then(async response => {
      if (response.status === 200) {
        alert("Logout successfully");
        return;
      }
      const message = await response.text();
      alert(`Error while logging out: ${message}`);
    });
  }
}