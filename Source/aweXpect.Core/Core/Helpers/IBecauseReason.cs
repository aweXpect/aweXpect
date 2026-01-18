using System.Threading.Tasks;
using aweXpect.Core.Constraints;

namespace aweXpect.Core.Helpers;

internal interface IBecauseReason
{
	
#if NET8_0_OR_GREATER
	public ValueTask<ConstraintResult>
#else
	public Task<ConstraintResult>
#endif
		ApplyTo(ConstraintResult result);
}
